/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */

using System;
using System.Collections.Generic;


namespace Entitas
{

	/// Keep basic Component's infomation, which allow Component->Index mapping at runtime
	public class ComponentInfo
	{
		public readonly Type type;
		public readonly Context context;
		public readonly int index;

		internal ComponentInfo(Type t, Context c, int i)
		{
			type = t;
			context = c;
			index = i;
		}
	}


	/// Generic infomation cache for each Component
	public class ComponentInfo<T> where T : IComponent
	{
		public static ComponentInfo info
		{
			get
			{
				// if no cached, lookup in ComponentInfoManager
				if (_info == null)
					_info = ComponentInfoManager.GetComponentInfo<T>();

				return _info;
			}
		}

		public static int index
		{
			get
			{
				var ii = info;
				return ii != null ? ii.index : -1;
			}
		}

		public static Context context
		{
			get
			{
				var ii = info;
				return ii != null ? ii.context : null;
			}
		}

		private static ComponentInfo _info;	// information cache
	}


	/// Global lookup for Components' infomations
	public static class ComponentInfoManager
	{
		private static Dictionary<Type, ComponentInfo> _componentLookup = new Dictionary<Type, ComponentInfo>();

		static ComponentInfoManager() {}


		public static ComponentInfo GetComponentInfo<T>() where T : IComponent
		{
			return GetComponentInfo(typeof(T));
		}

		public static ComponentInfo GetComponentInfo(Type type)
		{
			ComponentInfo info = null;
			_componentLookup.TryGetValue(type, out info);
			return info;
		}

		public static int GetComponentIndex<T>() where T : IComponent
		{
			return GetComponentIndex(typeof(T));
		}

		public static int GetComponentIndex(Type type)
		{
			var info = GetComponentInfo(type);
			return info != null ? info.index : -1;
		}

		// allow Context auto-register it's components
		internal static void RegisterComponent(Type t, Context c, int i)
		{
			_componentLookup[t] = new ComponentInfo(t, c, i);
		}
	}
}
