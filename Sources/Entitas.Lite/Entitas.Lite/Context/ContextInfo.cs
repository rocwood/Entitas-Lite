using System;

namespace Entitas
{
	public class ContextInfo
	{
		public readonly string[] componentNames;
		public readonly Type[] componentTypes;

		internal ContextInfo(Type[] componentTypes)
		{
			int count = componentTypes.Length;
			var componentNames = new string[count];

			for (int i = 0; i < count; i++)
				componentNames[i] = componentTypes[i].Name.RemoveComponentSuffix();

			this.componentNames = componentNames;
			this.componentTypes = componentTypes;
		}

		public int GetComponentCount() => componentTypes.Length;

		internal int GetComponentIndex<T>() where T : IComponent => GetComponentIndex(typeof(T));
		internal int GetComponentIndex(Type type) => Array.IndexOf(componentTypes, type);
	}
}
