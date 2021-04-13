using System;

namespace Entitas
{
	public class ComponentTypeInfo
	{
		public readonly Type type;
		public readonly int index;
		public readonly bool zeroSize;

		public ComponentTypeInfo(Type type, int index, bool zeroSize)
		{
			this.type = type;
			this.index = index;
			this.zeroSize = zeroSize;
		}
	}

	class ComponentTypeInfo<T> where T : IComponent
	{
		internal static int index = -1;
		internal static bool zeroSize = false;
	}
}
