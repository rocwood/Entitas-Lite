/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */

using UnityEngine;


namespace Entitas
{
	internal class FeatureWithObserver : Entitas.VisualDebugging.Unity.DebugSystems
	{
		public FeatureWithObserver(string name) : base(name)
		{
			FeatureHelper.CollectSystems(name, this);
			Object.DontDestroyOnLoad(this.gameObject);
		}
	}

	public static class FeatureObserverHelper
	{
		public static Systems Create()
		{
			return Create(DefaultFeature.Name);
		}

		public static Systems Create(string name)
		{
			if (!Application.isPlaying || !Application.isEditor)
				return new Feature(name);

			return new FeatureWithObserver(name);
		}
	}
}
