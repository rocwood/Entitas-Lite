using System;

namespace Entitas
{
	/// <summary>
	/// Compact bool array, for quick components matching 
	/// </summary>
	public class BitArray
	{
		private const int MaxLength = 0x7FEFFFFF;
		private const int BitsPerInt = sizeof(int)*8;

		private readonly int _length;
		private readonly int[] _items;

		public int Length => _length;

		public BitArray(int length = 0)
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

		public bool HasNoneOf(BitArray mask)
		{
			return !HasAnyOf(mask);
		}
	}
}
