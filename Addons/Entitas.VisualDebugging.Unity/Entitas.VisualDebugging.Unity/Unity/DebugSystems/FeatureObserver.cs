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
	}
}
