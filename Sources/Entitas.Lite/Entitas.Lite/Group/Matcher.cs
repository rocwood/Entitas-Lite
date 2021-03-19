#if false

using System.Collections.Generic;
using Entitas.Utils;

namespace Entitas
{
	public class Matcher
	{
		public readonly IReadOnlyList<int> all;
		public readonly IReadOnlyList<int> any;
		public readonly IReadOnlyList<int> none;

		internal Matcher(IReadOnlyList<int> all, IReadOnlyList<int> any, IReadOnlyList<int> none)
		{
			this.all = all;
			this.any = any;
			this.none = none;

			ComputeHashCode();
		}

		public bool Matches(Entity entity)
		{
			return (all == null || entity.HasAllComponents(all))
				&& (any == null || entity.HasAnyComponent(any))
				&& (none == null || !entity.HasAnyComponent(none));
		}

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != GetType())
				return false;

			return Equals((Matcher)obj);
		}

		public bool Equals(Matcher other)
		{
			if (other == null || other.GetHashCode() != GetHashCode())
				return false;

			if (!all.CheckEquals(other.all))	return false;
			if (!any.CheckEquals(other.any))	return false;
			if (!none.CheckEquals(other.none))	return false;

			return true;
		}

		private void ComputeHashCode()
		{
			var hashCode = -80052522;

			hashCode = all.ComputeHashCode(hashCode, -1521134295);
			hashCode = any.ComputeHashCode(hashCode, -1521134295);
			hashCode = none.ComputeHashCode(hashCode, -1521134295);

			_hash = hashCode;
		}

		public override int GetHashCode() => _hash;

		private int _hash;
	}
}

#endif
