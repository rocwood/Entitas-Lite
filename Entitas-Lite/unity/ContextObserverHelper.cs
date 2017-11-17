/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */


namespace Entitas
{
	public class ContextObserverHelper
	{
		public static void CreateAll(Contexts contexts)
		{
#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
			if (!UnityEngine.Application.isPlaying)
				return;

			foreach (var c in contexts.allContexts)
			{
				Create(c);
			}
#endif
		}

		public static void Create(IContext c)
		{
#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
			if (!UnityEngine.Application.isPlaying)
				return;

			var observer = new Entitas.VisualDebugging.Unity.ContextObserver(c);
			UnityEngine.Object.DontDestroyOnLoad(observer.gameObject);
#endif
		}
	}
}
