using UnityEngine;

namespace Entitas.VisualDebugging.Unity
{
	public static class ContextObserverHelper
	{
		public static void ObserveAll()
		{
			ObserveAll(Contexts.sharedInstance);
		}

		public static void ObserveAll(IContexts contexts)
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

			var observer = new ContextObserver(c);
			Object.DontDestroyOnLoad(observer.gameObject);
		}
	}
}
