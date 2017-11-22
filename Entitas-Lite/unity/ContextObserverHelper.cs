/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */

using UnityEngine;


namespace Entitas
{
	public static class ContextObserverHelper
	{
		public static void ObserveAll()
		{
			ObserveAll(Contexts.sharedInstance);
		}

		public static void ObserveAll(Contexts contexts)
		{
			if (!Application.isPlaying || !Application.isEditor)
				return;

			foreach (var c in contexts.allContexts)
			{
				Observe(c);
			}
		}

		public static void Observe(IContext c)
		{
			if (!Application.isPlaying || !Application.isEditor)
				return;

			var observer = new Entitas.VisualDebugging.Unity.ContextObserver(c);
			Object.DontDestroyOnLoad(observer.gameObject);
		}
	}
}
