using System;

namespace Entitas {

	public class ContextInfo {

		public readonly string name;
		public readonly string[] componentNames;
		public readonly Type[] componentTypes;

		public readonly bool isDefault = false;

		public ContextInfo(string name, string[] componentNames, Type[] componentTypes, bool isDefault = false) {
			this.name = name;
			this.componentNames = componentNames;
			this.componentTypes = componentTypes;
			this.isDefault = isDefault;
		}

		public ContextInfo(string name, Type[] componentTypes, bool isDefault = false) {
			int count = componentTypes.Length;
			string[] componentNames = new string[count];

			for (int i = 0; i < count; i++) {
				componentNames[i] = componentTypes[i].Name.RemoveComponentSuffix();
			}

			this.name = name;
			this.componentNames = componentNames;
			this.componentTypes = componentTypes;
			this.isDefault = isDefault;
		}

		internal int Find(Type type) {
			return Array.IndexOf(componentTypes, type);
		}

		internal int Find<T>() where T : IComponent {
			return Find(typeof(T));
		}
	}
}
