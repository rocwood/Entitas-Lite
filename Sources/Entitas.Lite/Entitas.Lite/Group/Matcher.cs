using System.Collections.Generic;
using Entitas.Utils;

namespace Entitas
{
	public class Matcher
	{
		private List<int> _all;
		private List<int> _any;
		private List<int> _none;

		internal bool isSealed { get; private set; }

		internal Matcher(bool isSealed, IReadOnlyList<int> all, IReadOnlyList<int> any, IReadOnlyList<int> none)
		{
			WithAll(all);
			WithAny(any);
			WithNone(none);

			if (isSealed)
				MakeSealed();
		}

		internal Matcher Clone()
		{
			return new Matcher(false, _all, _any, _none);
		}

		internal void WithAll(IReadOnlyList<int> indices) => Add(ref _all, indices);
		internal void WithAny(IReadOnlyList<int> indices) => Add(ref _any, indices);
		internal void WithNone(IReadOnlyList<int> indices) => Add(ref _none, indices);

		internal Matcher MakeSealed()
		{
			if (!isSealed)
			{
				ComputeHashCode();
				isSealed = true;
			}

			return this;
		}

		private void Add(ref List<int> list, IReadOnlyList<int> indices)
		{
			if (isSealed)
				return;

			if (indices == null)
				return;

			if (list == null)
				list = new List<int>(indices.Count);

			list.AddDistinctSorted(indices);
		}

		public bool Matches(Entity entity)
		{
			return (_all == null || entity.HasAllComponents(_all))
				&& (_any == null || entity.HasAnyComponent(_any))
				&& (_none == null || !entity.HasAnyComponent(_none));
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

			if (!_all.CheckEquals(other._all)) return false;
			if (!_any.CheckEquals(other._any)) return false;
			if (!_none.CheckEquals(other._none)) return false;

			return true;
		}

		private void ComputeHashCode()
		{
			var hashCode = -80052522;

			hashCode = _all.ComputeHashCode(hashCode, -1521134295);
			hashCode = _any.ComputeHashCode(hashCode, -1521134295);
			hashCode = _none.ComputeHashCode(hashCode, -1521134295);

			_hash = hashCode;
		}

		public override int GetHashCode() => _hash;

		private int _hash;
	}
}
