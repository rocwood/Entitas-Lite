using System;

namespace Entitas
{
	/// <summary>
	/// Compact bool array, for quick components matching 
	/// </summary>
	public class BitArray
	{
		public static readonly BitArray Empty = new BitArray(0);

		private const int MaxLength = 0x7FEFFFFF;
		private const int BitsPerInt = sizeof(int)*8;

		private readonly int _length;
		private readonly int[] _items;

		public int Length => _length;

		public BitArray(int length, int[] indices = null)
		{
			if (length < 0)
				length = 0;
			else if ((uint)length > MaxLength)
				length = MaxLength;

			_length = length;

			int size = (length > 0)
				? (((length - 1)/BitsPerInt) + 1) // ceil(length/32)
				: 0;

			_items = new int[size];

			if (indices != null)
				SetBitByIndex(indices, true);
		}

		public bool this[int index]
		{
			get {
				int d = _items[index / BitsPerInt];
				int mask = 1 << (index % BitsPerInt);
				return (d & mask) != 0;
			}

			set {
				ref int d = ref _items[index / BitsPerInt];
				int mask = 1 << (index % BitsPerInt);
				if (value)
					d |= mask;
				else
					d &= ~mask;
			}
		}

		public void SetBitByIndex(int[] indices, bool value)
		{
			if (indices == null || indices.Length <= 0)
				return;

			for (int i = 0; i < indices.Length; i++)
			{
				int index = indices[i];
				if (index < 0 || index >= _length)
					continue;

				this[index] = value;
			}
		}

		public void Union(BitArray other)
		{
			if (other == null)
				return;

			int size = Math.Min(_items.Length, other._items.Length);

			for (int i = 0; i < size; i++)
				_items[i] |= other._items[i];
		}

		public void Intersection(BitArray other)
		{
			if (other == null)
				return;

			int size = Math.Min(_items.Length, other._items.Length);

			for (int i = 0; i < size; i++)
				_items[i] &= other._items[i];
		}

		public bool IsEmpty()
		{
			if (_length <= 0)
				return true;

			int size = _items.Length;

			for (int i = 0; i < size; i++)
			{
				if (_items[i] != 0)
					return false;
			}

			return true;
		}

		public bool HasAllOf(BitArray mask)
		{
			if (mask == null)
				return true;

			if (mask.Length > _length)
				return false;

			int size = mask._items.Length;
		
			for (int i = 0; i < size; i++)
			{
				int v = mask._items[i];
				if (v == 0)
					continue;

				if ((_items[i] & v) != v)
					return false;
			}

			return true;
		}

		public bool HasAnyOf(BitArray mask)
		{
			if (mask == null)
				return false;

			int size = Math.Min(_items.Length, mask._items.Length);

			for (int i = 0; i < size; i++)
			{
				int v = mask._items[i];
				if (v == 0)
					continue;

				if ((_items[i] & v) != 0)
					return true;
			}

			return false;
		}

		public bool Equals(BitArray other)
		{
			if (other == null)
				return false;

			if (_length != other.Length)
				return false;

			int size = _items.Length;

			for (int i = 0; i < size; i++)
			{
				if (_items[i] != other._items[i])
					return false;
			}

			return true;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != GetType())
				return false;

			return Equals((BitArray)obj);
		}

		public override int GetHashCode()
		{
			int hashCode = 994075540;
			hashCode = hashCode * -1521134295 + _length.GetHashCode();

			for (int i = 0; i < _items.Length; i++)
				hashCode = hashCode * -1521134295 + _items[i];
			
			return hashCode;
		}

		public static bool Equals(BitArray x, BitArray y)
		{
			if (x == null && y == null)
				return true;

			if (x != null && y != null)
				return x.Equals(y);

			return false;
		}
	}
}
