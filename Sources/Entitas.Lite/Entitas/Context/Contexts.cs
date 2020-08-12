using System;
using System.Collections.Generic;
using System.Linq;

namespace Entitas
{
	/// A singleton Contexts, with name-Context lookup, and a defaultContext
	public class Contexts : IContexts
	{
		public static int startCreationIndex = 1;
		public static bool useSafeAERC = true;

		private static Contexts _sharedInstance;

		public static Contexts sharedInstance
		{
			get
			{
				if (_sharedInstance == null)
					_sharedInstance = new Contexts();
				return _sharedInstance;
			}
		}

		public static void DestroyInstance()
		{
			_sharedInstance = null;
		}

		private Dictionary<string, Context> _contextLookup;
		private Context[] _contextList;
		private Context _defaultContext;

		public IContext[] allContexts { get { return _contextList; } }
		public Context defaultContext { get { return _defaultContext; } }

		public Context GetContext<S>() where S:ContextAttribute { return GetContext(ContextAttribute.GetName<S>()); }
		public Context GetContext(string contextName) { return _contextLookup[contextName]; }

		public static Context Default { get { return sharedInstance.defaultContext; } }
		public static Context Get<S>() where S:ContextAttribute { return sharedInstance.GetContext<S>(); }
		public static Context Get(string contextName) { return sharedInstance.GetContext(contextName); }


		public Contexts()
		{
			InitAllContexts();
		}

		public void Reset()
		{
			int count = _contextList.Length;
			for (int i = 0; i < count; i++)
			{
				_contextList[i].Reset();
			}
		}

		/// Build contexts' list and lookup according to collected Component-Types
		private void InitAllContexts()
		{
			var defaultContextName = ContextAttribute.GetName<Default>();

			var comps = CollectAllComponents();

			var contextList = new List<Context>();
			_contextLookup = new Dictionary<string, Context>();

			foreach (var cc in comps)
			{
				var name = cc.Key;
				var list = cc.Value;
				var isDefault = name == defaultContextName;

				list.Sort((x, y) => string.CompareOrdinal(x.FullName, y.FullName));

				var c = new Context(list.Count, startCreationIndex, new ContextInfo(name, list.ToArray(), isDefault), GetAERC());

				_contextLookup[name] = c;
				contextList.Add(c);
			}

			_defaultContext = _contextLookup[defaultContextName];
			_contextList = contextList.ToArray();
		}

		/// Collect all Compoent-Types in current domain
		private static Dictionary<string, List<Type>> CollectAllComponents()
		{
			var defaultContextName = ContextAttribute.GetName<Default>();

			var compType = typeof(IComponent);
			var types = AppDomain.CurrentDomain.GetAssemblies()
								.SelectMany(s => s.GetTypes())
								.Where(p => p.IsClass && p.IsPublic && !p.IsAbstract &&
											compType.IsAssignableFrom(p));

			var comps = new Dictionary<string, List<Type>>();
			comps[defaultContextName] = new List<Type>();

			var attrType = typeof(ContextAttribute);

			foreach (var t in types)
			{
				var attribs = t.GetCustomAttributes(attrType, false);

				if (attribs == null || attribs.Length <= 0)
				{
					CollectComponent(comps, defaultContextName, t);
				}
				else
				{
					foreach (var attr in attribs)
					{
						CollectComponent(comps, ((ContextAttribute)attr).name, t);
					}
				}
			}

			return comps;
		}

		private static void CollectComponent(Dictionary<string, List<Type>> comps, string name, Type t)
		{
			List<Type> list;
			if (!comps.TryGetValue(name, out list))
			{
				list = new List<Type>();
				comps[name] = list;
			}

			list.Add(t);
		}

		private static Func<IEntity, IAERC> GetAERC()
		{
			if (useSafeAERC)
				return (entity) => new SafeAERC(entity);
			else
				return (entity) => new UnsafeAERC();
		}

	}
}
