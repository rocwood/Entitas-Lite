using System.Collections.Generic;

namespace Entitas.Utils
{
	static class IndicesUtils
	{
		public static void AddDistinctSorted(this IList<int> sortedIndices, IReadOnlyList<int> indices)
		{
			if (indices == null || indices.Count == 0)
				return;

			for (int i = 0; i < indices.Count; i++)
				AddDistinctSorted(sortedIndices, indices[i]);
		}

		public static void AddDistinctSorted(this IList<int> sortedIndices, int value)
		{
			int lo = 0;
			int hi = sortedIndices.Count - 1;

			while (lo <= hi)
			{
				int i = lo + ((hi - lo) >> 1);
				int c = sortedIndices[i];

				if (c == value)
					return;
				
				if (c < value)
					lo = i + 1;
				else
					hi = i - 1;
			}

			sortedIndices.Insert(lo, value);
		}

		public static bool CheckEquals(this IReadOnlyList<int> indices1, IReadOnlyList<int> indices2)
		{
			if (indices1 == null && indices2 == null)
				return true;

			if (indices1 != null && indices2 != null)
			{
				if (indices1.Count != indices2.Count)
					return false;

				for (int i = 0; i < indices1.Count; i++)
				{
					if (indices1[i] != indices2[i])
						return false;
				}

				return true;
			}

			return false;
		}

		public static int ComputeHashCode(this IReadOnlyList<int> indices, int hashCode, int multiply)
		{
			if (indices != null)
			{
				for (int i = 0; i < indices.Count; i++)
					hashCode = hashCode * multiply + indices[i];
			}

			return hashCode;
		}
	}
}
