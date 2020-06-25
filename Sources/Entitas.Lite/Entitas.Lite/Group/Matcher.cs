using System.Collections.Generic;
using Entitas.Utils;

namespace Entitas
{
	public class Matcher
	{
		public readonly IReadOnlyList<int> allOfIndices;
		public readonly IReadOnlyList<int> anyOfIndices;
		public readonly IReadOnlyList<int> noneOfIndices;

		public bool Matches(Entity entity)
		{
			return (allOfIndices == null || entity.HasAllComponents(allOfIndices))
				&& (anyOfIndices == null || entity.HasAnyComponent(anyOfIndices))
				&& (noneOfIndices == null || !entity.HasAnyComponent(noneOfIndices));
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

			if (!allOfIndices.CheckEquals(other.allOfIndices))
				return false;
			if (!anyOfIndices.CheckEquals(other.anyOfIndices))
				return false;
			if (!noneOfIndices.CheckEquals(other.noneOfIndices))
				return false;

			return true;
		}

		internal Matcher(IReadOnlyList<int> allOfIndices, IReadOnlyList<int> anyOfIndices, IReadOnlyList<int> noneOfIndices)
		{
			this.allOfIndices = allOfIndices;
			this.anyOfIndices = anyOfIndices;
			this.noneOfIndices = noneOfIndices;
			
			ComputeHashCode();
		}

		private void ComputeHashCode()
		{
			var hashCode = -80052522;

			hashCode = allOfIndices.ComputeHashCode(hashCode, -1521134295);
			hashCode = anyOfIndices.ComputeHashCode(hashCode, -1521134295);
			hashCode = noneOfIndices.ComputeHashCode(hashCode, -1521134295);

			_hash = hashCode;
		}

		public override int GetHashCode() => _hash;

		private int _hash;

		/*
		internal IReadOnlyList<int> combineIndices
		{
			get {
				if (!_combineIndicesCached)
				{
					if (_combineIndices == null)
						_combineIndices = new List<int>(ContextProvider.GetComponentCount());

					_combineIndices.Clear();

					_combineIndices.MergeDistinctSorted(_allOfIndices);
					_combineIndices.MergeDistinctSorted(_anyOfIndices);
					_combineIndices.MergeDistinctSorted(_noneOfIndices);

					_combineIndicesCached = true;
				}

				return _combineIndices;
			}
		}

		private bool _combineIndicesCached;
		private List<int> _combineIndices;
		*/
	}
}
