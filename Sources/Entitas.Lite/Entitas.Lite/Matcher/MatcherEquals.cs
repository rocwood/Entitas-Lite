using Entitas.Utils;

namespace Entitas
{
	public partial class Matcher
	{
		int _hash;
		bool _isHashCached;

		public bool Equals(Matcher other)
		{
			if (other == null || other.GetHashCode() != GetHashCode())
				return false;

			if (!BitArray.Equals(_allOfMask, other._allOfMask))
				return false;
			if (!BitArray.Equals(_anyOfMask, other._anyOfMask))
				return false;
			if (!BitArray.Equals(_noneOfMask, other._noneOfMask))
				return false;

			return true;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != GetType())
				return false;

			return Equals((Matcher)obj);
		}

		public override int GetHashCode()
		{
			if (!_isHashCached)
			{
				var hashCode = -80052522;

				if (_allOfMask != null)
					hashCode = hashCode * -1521134295 + _allOfMask.GetHashCode();
				if (_anyOfMask != null)
					hashCode = hashCode * -1521134295 + _anyOfMask.GetHashCode();
				if (_noneOfMask != null)
					hashCode = hashCode * -1521134295 + _noneOfMask.GetHashCode();

				_hash = hashCode;
				_isHashCached = true;
			}

			return _hash;
		}
	}
}
