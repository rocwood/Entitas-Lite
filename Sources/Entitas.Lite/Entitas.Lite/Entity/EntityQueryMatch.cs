using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Entitas
{
	public partial class Entity
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool IsMatch(IReadOnlyList<int> indices, bool matchAny)
		{
			return matchAny ? IsMatchAny(indices) : IsMatchAll(indices);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsMatchAll(IReadOnlyList<int> indices)
		{
			for (int i = 0; i < indices.Count; i++)
			{
				var index = indices[i];
				if (index >= 0)
				{
					var c = _components[index];
					if (c == null)
						return false;
				}
				else
				{
					var c = _components[-index - 1];
					if (c != null)
						return false;
				}
			}

			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsMatchAny(IReadOnlyList<int> indices)
		{
			bool hasAny = false;

			for (int i = 0; i < indices.Count; i++)
			{
				var index = indices[i];
				if (index >= 0)
				{
					if (!hasAny)
					{
						var c = _components[index];
						if (c != null)
							hasAny = true;
					}
				}
				else
				{
					var c = _components[-index - 1];
					if (c != null)
						return false;
				}
			}

			return hasAny;
		}
	}
}
