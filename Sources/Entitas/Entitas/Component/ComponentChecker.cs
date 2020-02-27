using System;
using System.Linq;
using System.Reflection;

namespace Entitas
{
	public static class ComponentChecker
	{
		public static bool IsZeroSize<T>() where T:class, IComponent
		{
			return IsZeroSize(typeof(T));
		}

		public static bool IsZeroSize(Type type)
		{
			for (;;)
			{
				if (type == null || type == typeof(object))
					return true;

				// check members
				var members = type.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
				if (members.Any(m => m.MemberType != MemberTypes.Constructor))
					return false;

				// check parent
				type = type.BaseType;
			}
		}
	}
}
