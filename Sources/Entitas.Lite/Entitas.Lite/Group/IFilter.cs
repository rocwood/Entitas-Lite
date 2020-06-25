using System.Collections.Generic;

namespace Entitas
{
	public interface IFilter
	{
		void AllOf(IReadOnlyList<int> indices);
		void AnyOf(IReadOnlyList<int> indices);
		void NoneOf(IReadOnlyList<int> indices);

		void AllOf(params int[] indices);
		void AnyOf(params int[] indices);
		void NoneOf(params int[] indices);
	}
}
