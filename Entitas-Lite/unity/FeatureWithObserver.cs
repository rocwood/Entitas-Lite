/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */


namespace Entitas
{
#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
	public class FeatureWithObserver : Entitas.VisualDebugging.Unity.DebugSystems
	{
		public FeatureWithObserver() : base(DefaultFeature.Name)
		{
			Init(DefaultFeature.Name);
		}

		public FeatureWithObserver(string name) : base(name)
		{
			Init(name);
		}

		private void Init(string name)
		{
			FeatureHelper.CollectSystems(name, this);
			UnityEngine.Object.DontDestroyOnLoad(this.gameObject);
		}
	}
#else
	using FeatureWithObserver = Feature;
#endif
}
