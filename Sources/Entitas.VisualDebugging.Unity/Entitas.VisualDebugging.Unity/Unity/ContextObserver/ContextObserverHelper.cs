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

		public static void ClearAll()
		{
			if (!Application.isPlaying || !Application.isEditor)
				return;

			var behaviours = Object.FindObjectsOfType<ContextObserverBehaviour>();
			if (behaviours != null && behaviours.Length > 0)
			{
				foreach (var behaviour in behaviours)
				{
					Object.Destroy(behaviour.gameObject);
				}
			}
		}
	}
}
