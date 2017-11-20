/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */

using System;
using System.Collections.Generic;
using System.Linq;


namespace Entitas
{
	/// A singleton Contexts, with name-Context lookup, and a defaultContext
	public class Contexts : IContexts
	{
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

		private Dictionary<string, Context> _contextLookup;
		private Context[] _contextList;
		public Context _defaultContext;
		
		public IContext[] allContexts { get { return _contextList; } }
		public Context defaultContext { get { return _defaultContext; } }

		public Context GetContext<S>() where S:ContextScope { return _contextLookup[ContextScopeHelper.GetName<S>()]; }
		public Context GetContext(string contextName) { return _contextLookup[contextName]; }


		public static Context Default { get { return sharedInstance.defaultContext; } }
		public static Context Get<S>() where S : ContextScope { return sharedInstance._contextLookup[ContextScopeHelper.GetName<S>()]; }
		public static Context Get(string contextName) { return sharedInstance._contextLookup[contextName]; }


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
			var comps = CollectAllComponents();

			var contextList = new List<Context>();
			_contextLookup = new Dictionary<string, Context>();

			foreach (var cc in comps)
			{
				var name = cc.Key;
				var list = cc.Value;

				var c = new Context(name, list.ToArray());

				_contextLookup[name] = c;
				contextList.Add(c);
			}

			_defaultContext = _contextLookup[DefaultContext.Name];
			_contextList = contextList.ToArray();
		}

		/// Collect all Compoent-Types in current domain
		private static Dictionary<string, List<Type>> CollectAllComponents()
		{
			var compType = typeof(IComponent);
			var types = AppDomain.CurrentDomain.GetAssemblies()
								.SelectMany(s => s.GetTypes())
								.Where(p => p.IsClass && p.IsPublic && !p.IsAbstract && 
											compType.IsAssignableFrom(p));

			Dictionary<string, List<Type>> comps = new Dictionary<string, List<Type>>();
			comps[DefaultContext.Name] = new List<Type>();

			var scopeType = typeof(ContextScope);

			foreach (var t in types)
			{
				var scopes = t.GetCustomAttributes(scopeType, false);

				if (scopes == null || scopes.Length <= 0)
				{
					CollectComponent(comps, DefaultContext.Name, t);
				}
				else
				{
					foreach (var scope in scopes)
					{
						string name = ContextScopeHelper.GetName(scope.GetType());
						CollectComponent(comps, name, t);
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
	}
}
