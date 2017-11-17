/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */

using System;


namespace Entitas
{
	/// Extension and Helper for ContextInfo
	public static class ContextInfoHelper
	{
		internal static int IndexOf(this ContextInfo c, Type type)
		{
			return Array.IndexOf(c.componentTypes, type);
		}

		internal static int IndexOf<T>(this ContextInfo info) where T : IComponent
		{
			return info.IndexOf(typeof(T));
		}

		internal static ContextInfo Make(string name, Type[] componentTypes)
		{
			int count = componentTypes.Length;
			string[] componentNames = new string[count];

			for (int i = 0; i < count; i++)
			{
				componentNames[i] = componentTypes[i].Name;
			}

			return new ContextInfo(name, componentNames, componentTypes);
		}
	}
}
