using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Entitas
{
	public class EntityTable
	{
		private struct Entry
		{
			public int hashCode;	// Lower 31 bits of hash code, -1 if unused
			public int next;		// Index of next entry, -1 if last
			public int key;			// Key of entry
			public Entity value;	// Value of entry
		}

		private int[] buckets;
		private Entry[] entries;

		private int count;
		private int freeList;
		private int freeCount;

		private int version;

		public EntityTable(int capacity = 0)
		{
			if (capacity <= 0)
				capacity = 0;

			Initialize(capacity);
		}

		public int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return count - freeCount; }
		}

		public Entity this[int key]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get {
				int i = FindEntry(key);
				if (i >= 0) return entries[i].value;
				return null;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(Entity value)
		{
			if (value == null)
				return;

			Insert(value.id, value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			if (count > 0)
			{
				for (int i = 0; i < buckets.Length; i++) buckets[i] = -1;
				Array.Clear(entries, 0, count);
				freeList = -1;
				count = 0;
				freeCount = 0;
				version++;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(int key)
		{
			return FindEntry(key) >= 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CopyTo(IList<Entity> output)
		{
			int count = this.count;
			var entries = this.entries;

			for (int i = 0; i < count; i++)
			{
				if (entries[i].hashCode >= 0)
					output.Add(entries[i].value);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Enumerator GetEnumerator()
		{
			return new Enumerator(this);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int FindEntry(int key)
		{
			if (key <= 0)
				return -1;

			if (buckets != null)
			{
				int hashCode = key & 0x7FFFFFFF;
				for (int i = buckets[hashCode % buckets.Length]; i >= 0; i = entries[i].next)
				{
					if (entries[i].hashCode == hashCode && entries[i].key == key) return i;
				}
			}
			return -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Initialize(int capacity)
		{
			int size = HashHelpers.GetPrime(capacity);
			buckets = new int[size];
			for (int i = 0; i < buckets.Length; i++) buckets[i] = -1;
			entries = new Entry[size];
			freeList = -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Insert(int key, Entity value)
		{
			if (key <= 0 || value == null)
				return;

			int hashCode = key & 0x7FFFFFFF;
			int targetBucket = hashCode % buckets.Length;

			for (int i = buckets[targetBucket]; i >= 0; i = entries[i].next)
			{
				if (entries[i].hashCode == hashCode && entries[i].key == key)
				{
					entries[i].value = value;
					version++;
					return;
				}
			}
			int index;
			if (freeCount > 0)
			{
				index = freeList;
				freeList = entries[index].next;
				freeCount--;
			}
			else
			{
				if (count == entries.Length)
				{
					Resize();
					targetBucket = hashCode % buckets.Length;
				}
				index = count;
				count++;
			}

			entries[index].hashCode = hashCode;
			entries[index].next = buckets[targetBucket];
			entries[index].key = key;
			entries[index].value = value;
			buckets[targetBucket] = index;
			version++;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Resize()
		{
			Resize(HashHelpers.ExpandPrime(count));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Resize(int newSize)
		{
			int[] newBuckets = new int[newSize];
			for (int i = 0; i < newBuckets.Length; i++) newBuckets[i] = -1;
			Entry[] newEntries = new Entry[newSize];
			Array.Copy(entries, 0, newEntries, 0, count);

			for (int i = 0; i < count; i++)
			{
				if (newEntries[i].hashCode >= 0)
				{
					int bucket = newEntries[i].hashCode % newSize;
					newEntries[i].next = newBuckets[bucket];
					newBuckets[bucket] = i;
				}
			}
			buckets = newBuckets;
			entries = newEntries;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Remove(int key)
		{
			if (key <= 0)
				return false;

			if (buckets != null)
			{
				int hashCode = key & 0x7FFFFFFF;
				int bucket = hashCode % buckets.Length;
				int last = -1;
				for (int i = buckets[bucket]; i >= 0; last = i, i = entries[i].next)
				{
					if (entries[i].hashCode == hashCode && entries[i].key == key)
					{
						if (last < 0)
						{
							buckets[bucket] = entries[i].next;
						}
						else
						{
							entries[last].next = entries[i].next;
						}
						entries[i].hashCode = -1;
						entries[i].next = freeList;
						entries[i].key = 0;
						entries[i].value = null;
						freeList = i;
						freeCount++;
						version++;
						return true;
					}
				}
			}
			return false;
		}

		public struct Enumerator : IEnumerator<Entity>
		{
			private EntityTable container;
			private int index;
			private int version;
			private Entity currentValue;

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal Enumerator(EntityTable container)
			{
				this.container = container;
				this.version = container.version;
				index = 0;
				currentValue = null;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Dispose()
			{
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				if (version != container.version)
					throw new InvalidOperationException("EnumFailedVersion");

				while ((uint)index < (uint)container.count)
				{
					if (container.entries[index].hashCode >= 0)
					{
						currentValue = container.entries[index].value;
						index++;
						return true;
					}
					index++;
				}

				index = container.count + 1;
				currentValue = null;
				return false;
			}

			public Entity Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get {
					return currentValue;
				}
			}

			object IEnumerator.Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get {
					return currentValue;
				}
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			void IEnumerator.Reset()
			{
				if (version != container.version)
					throw new InvalidOperationException("EnumFailedVersion");

				index = 0;
				currentValue = null;
			}
		}
	}

	static class HashHelpers
	{
		// Table of prime numbers to use as hash table sizes. 
		// A typical resize algorithm would pick the smallest prime number in this array
		// that is larger than twice the previous capacity. 
		// Suppose our Hashtable currently has capacity x and enough elements are added 
		// such that a resize needs to occur. Resizing first computes 2x then finds the 
		// first prime in the table greater than 2x, i.e. if primes are ordered 
		// p_1, p_2, ..., p_i, ..., it finds p_n such that p_n-1 < 2x < p_n. 
		// Doubling is important for preserving the asymptotic complexity of the 
		// hashtable operations such as add.  Having a prime guarantees that double 
		// hashing does not lead to infinite loops.  IE, your hash function will be 
		// h1(key) + i*h2(key), 0 <= i < size.  h2 and the size must be relatively prime.
		public static readonly int[] primes = {
			3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
			1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591,
			17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437,
			187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263,
			1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369};

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsPrime(int candidate)
		{
			if ((candidate & 1) != 0)
			{
				int limit = (int)Math.Sqrt(candidate);
				for (int divisor = 3; divisor <= limit; divisor += 2)
				{
					if ((candidate % divisor) == 0)
						return false;
				}
				return true;
			}
			return (candidate == 2);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetPrime(int min)
		{
			if (min < 0)
				min = 0;

			for (int i = 0; i < primes.Length; i++)
			{
				int prime = primes[i];
				if (prime >= min) return prime;
			}

			// outside of our predefined table. compute the hard way. 
			for (int i = (min | 1); i < int.MaxValue; i += 2)
			{
				if (IsPrime(i))
					return i;
			}
			return min;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ExpandPrime(int oldSize)
		{
			int newSize = 2 * oldSize;

			// Allow the hashtables to grow to maximum possible size (~2G elements) before encoutering capacity overflow.
			// Note that this check works even when _items.Length overflowed thanks to the (uint) cast
			if ((uint)newSize > MaxPrimeArrayLength && MaxPrimeArrayLength > oldSize)
				return MaxPrimeArrayLength;

			return GetPrime(newSize);
		}

		// This is the maximum prime smaller than Array.MaxArrayLength
		public const int MaxPrimeArrayLength = 0x7FEFFFFD;
	}
}
