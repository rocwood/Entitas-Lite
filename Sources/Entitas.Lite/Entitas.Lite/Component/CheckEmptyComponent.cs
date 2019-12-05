using System;
using System.Linq;
using System.Reflection;

namespace Entitas
{
	/// <summary>
	/// Check whether component is empty or zero-size, without members
	/// </summary>
	public static class CheckEmptyComponent
	{
		public static bool IsEmpty<T>() where T:class, IComponent
		{
			return Checker<T>.GetResult();
		}

		static class Checker<T> where T:class, IComponent
		{
			private static bool? _cachedResult;

			public static bool GetResult()
			{
				if (!_cachedResult.HasValue)
					_cachedResult = Check(typeof(T));

				return _cachedResult.Value;
			}

			private static bool Check(Type type)
			{
				if (type == null || type == typeof(object))
					return true;

				var members = type.GetMembers(BindingFlags.Instance|BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.DeclaredOnly);
				int memberCount = members.Where(
					m => m.MemberType != MemberTypes.Constructor).Count();

				if (memberCount > 0)
					return false;

				var baseType = type.BaseType;
				if (baseType == null || baseType == typeof(object))
					return true;

				return Check(baseType);
			}
		}
	}
}
