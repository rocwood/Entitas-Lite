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

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
	public class ContextAttribute : Attribute
	{
		public const string DefaultName = "";
		public readonly string contextName;

		public ContextAttribute(string name = DefaultName) { contextName = name; }
	}


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


		public Contexts()
		{
			InitContexts();
			CreateObservers();
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
		private void InitContexts()
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

			_defaultContext = _contextLookup[ContextAttribute.DefaultName];
			_contextList = contextList.ToArray();
		}

		/// Collect all Compoent-Types in current domain
		private Dictionary<string, List<Type>> CollectAllComponents()
		{
			var compType = typeof(IComponent);
			var types = AppDomain.CurrentDomain.GetAssemblies()
								.SelectMany(s => s.GetTypes())
								.Where(p => p.IsClass && p.IsPublic && !p.IsAbstract && compType.IsAssignableFrom(p));

			Dictionary<string, List<Type>> comps = new Dictionary<string, List<Type>>();
			comps[ContextAttribute.DefaultName] = new List<Type>();

			var attribType = typeof(ContextAttribute);

			foreach (var p in types)
			{
				var attribs = p.GetCustomAttributes(attribType, false);

				string name = (attribs == null || attribs.Length <= 0) 
					? ContextAttribute.DefaultName
					: ((ContextAttribute)attribs[0]).contextName;

				List<Type> list;
				if (!comps.TryGetValue(name, out list))
				{
					list = new List<Type>();
					comps[name] = list;
				}

				list.Add(p);
			}

			return comps;
		}

		private void CreateObservers()
		{
#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
			if (UnityEngine.Application.isPlaying)
			{
				int count = _contextList.Length;
				for (int i = 0; i < count; i++)
				{
					var observer = new VisualDebugging.Unity.ContextObserver(_contextList[i]);
					UnityEngine.Object.DontDestroyOnLoad(observer.gameObject);
				}
			}
#endif
		}
	}
}
