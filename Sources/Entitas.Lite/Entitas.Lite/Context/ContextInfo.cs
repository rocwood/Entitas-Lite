using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Entitas
{
	public static class ContextInfo
	{
		private static ComponentTypeInfo[] _componentInfoList;


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ComponentTypeInfo[] GetComponentInfoList()
		{
			if (_componentInfoList == null)
				_componentInfoList = CollectComponents();

			return _componentInfoList;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int GetIndexOf<T>() where T : IComponent
		{
			return ComponentTypeInfo<T>.index;
		}

		private static ComponentTypeInfo[] CollectComponents()
		{
			var types = AppDomain.CurrentDomain
						.GetAssemblies()
						.Where(s => !s.FullName.StartsWith("System.") && !s.FullName.StartsWith("Entitas."))
						.SelectMany(s => s.GetTypes())
						.Where(p => p.IsClass && p.IsPublic && !p.IsAbstract && typeof(IComponent).IsAssignableFrom(p))
						.ToArray();

			Array.Sort(types, (x, y) => string.CompareOrdinal(x.FullName, y.FullName));

			var list = new ComponentTypeInfo[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				var t = types[i];
				var typeInfo = new ComponentTypeInfo(t, i, IsZeroSize(t));

				list[i] = typeInfo;

				var infoType = typeof(ComponentTypeInfo<>).MakeGenericType(t);

				var fieldIndex = infoType.GetField("index", BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic);
				fieldIndex?.SetValue(null, i);
				var fieldZeroSize = infoType.GetField("zeroSize", BindingFlags.Static|BindingFlags.Public|BindingFlags.NonPublic);
				fieldZeroSize?.SetValue(null, typeInfo.zeroSize);
			}

			return list;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsZeroSize(Type type)
		{
			for (; ; )
			{
				if (type == null || type == typeof(object))
					return true;

				var members = type.GetMembers(BindingFlags.Instance|BindingFlags.Public|BindingFlags.NonPublic|BindingFlags.DeclaredOnly);
				if (members.Any(m => m.MemberType != MemberTypes.Constructor))
					return false;

				type = type.BaseType;
			}
		}
	}
}
