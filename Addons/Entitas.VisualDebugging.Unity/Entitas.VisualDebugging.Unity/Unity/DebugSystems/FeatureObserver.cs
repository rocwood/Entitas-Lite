using UnityEngine;

namespace Entitas.VisualDebugging.Unity
{
	public class FeatureWithObserver : DebugSystems
	{
		public FeatureWithObserver(string name) : base(FeatureHelper.GetUnnamed(name))
		{
			FeatureHelper.CollectSystems(this.name, this);
			Object.DontDestroyOnLoad(this.gameObject);
		}
	}

	public static class FeatureObserverHelper
	{
		public static Systems CreateFeature(string name)
		{
			if (!Application.isPlaying || !Application.isEditor)
				return new Feature(name);

			return new FeatureWithObserver(name);
		}

		public static void ClearAll()
		{
			if (!Application.isPlaying || !Application.isEditor)
				return;

			var behaviours = GameObject.FindObjectsOfType<DebugSystemsBehaviour>();
			if (behaviours != null && behaviours.Length > 0)
			{
				foreach (var behaviour in behaviours)
				{
					GameObject.Destroy(behaviour);
				}
			}
		}
	}
}
