using Entitas;
using System;
using System.Collections.Generic;
using System.Threading;

#if !CONSOLE_APP
using UnityEngine;
#endif

namespace Example
{
	[Default]
	public class PositionComponent : IComponent
	{
		public int x;
		public int y;
	}

	// if no context declaration, it comes into Default context
	public class VelocityComponent : IComponent
	{
		public int x;
		public int y;

		// don't be afraid of writing helper accessor
		public void SetValue(int nx, int ny)
		{
			x = nx;
			y = ny;
		}
	}

	// if no feature-set declaration, it comes into UnnamedFeature
	public class MovementSystem : IExecuteSystem
	{
		public void Execute()
		{
			// new API for getting group with all matched entities from context
			var entities = Context<Default>.AllOf<PositionComponent, VelocityComponent>().GetEntities();

			foreach (var e in entities)
			{
				var vel = e.Get<VelocityComponent>();
				var pos = e.Modify<PositionComponent>(); // new API for trigger Monitor/ReactiveSystem

				pos.x += vel.x;
				pos.y += vel.y;
			}
		}
	}

	// Sample view just display Entity's Position if changed
	public class ViewSystem : ReactiveSystem
	{
		public ViewSystem()
		{
			// new API, add monitor that watch Position changed and call Process 
			monitors += Context<Default>.AllOf<PositionComponent>().OnAdded(Process);
		}

		protected void Process(List<Entity> entities)
		{
			foreach (var e in entities)
			{
				var pos = e.Get<PositionComponent>();
#if CONSOLE_APP
				Console.WriteLine(
#else
				Debug.Log(
#endif
					"Entity" + e.creationIndex + ": x=" + pos.x + " y=" + pos.y);
			}
		}
	}

#if CONSOLE_APP
	public class GameController
#else
	public class GameController : MonoBehaviour
#endif
	{
		private Systems _feature;

		public void Start()
		{
			var contexts = Contexts.sharedInstance;

#if UNITY_EDITOR
			ContextObserverHelper.ObserveAll(contexts);
#endif

			// create random entity
			var rand = new System.Random();
			var context = Contexts.Default;
			var e = context.CreateEntity();
				e.Add<PositionComponent>();
				e.Add<VelocityComponent>().SetValue(rand.Next()%10, rand.Next()%10);

#if UNITY_EDITOR
			_feature = FeatureObserverHelper.CreateFeature(null);
#else
			// init systems, auto collect matched systems, no manual Systems.Add(ISystem) required
			_feature = new Feature(null);
#endif
			_feature.Initialize();
		}

		public void Update()
		{
			_feature.Execute();
			_feature.Cleanup();
		}
	}

#if CONSOLE_APP
	public class Program
	{
		static void Main(string[] args)
		{
			var game = new GameController();

			game.Start();

			Console.WriteLine("Press ESC to exit ...");
			Console.WriteLine();

			// main lopp for game
			while (true)
			{
				game.Update();

				Thread.Sleep(500);

				if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
					break;
			}
		}
	}
#else
	public class Program : GameController
	{
	}
#endif
}
