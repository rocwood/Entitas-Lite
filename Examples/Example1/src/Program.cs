using Entitas;
using System;
using System.Collections.Generic;
using System.Threading;

#if !CONSOLE_APP
using UnityEngine;
#endif

namespace Example1
{

	public class PositionComponent : IComponent
	{
		public int x;
		public int y;
	}

	public class VelocityComponent : IComponent
	{
		public int x;
		public int y;

		public void SetValue(int nx, int ny)
		{
			x = nx;
			y = ny;
		}
	}

	public class MovementSystem : IExecuteSystem
	{
		public void Execute()
		{
			var context = Contexts.Default;

			var entities = context.GetEntities(Matcher<Default>.AllOf<PositionComponent, VelocityComponent>());
			foreach (var e in entities)
			{
				var vel = e.Get<VelocityComponent>();
				var pos = e.Modify<PositionComponent>();

				pos.x += vel.x;
				pos.y += vel.y;
			}
		}
	}


	// Sample view just display Entity's Position if changed
	public class ViewSystem : ReactiveSystem
	{
		public ViewSystem() 
			: base(Contexts.Default.CreateCollector(Matcher<Default>.AllOf<PositionComponent>()))
		{
		}
	
		protected override void Execute(List<Entity> entities)
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

			// create random entity
			var rand = new System.Random();
			var context = Contexts.Default;
			var e = context.CreateEntity();
				e.Add<PositionComponent>();
				e.Add<VelocityComponent>().SetValue(rand.Next()%10, rand.Next()%10);

			// init systems
			_feature = new Feature(null);
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
