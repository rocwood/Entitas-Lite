using System;
using System.Diagnostics;

namespace Entitas.Benchmark
{
	public class Position : IComponent, IModifiable
	{
		public float x;
		public float y;

		public void Set(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public bool modified { get; set; }
	}

	public class Velocity : IComponent
	{
		public float x;
		public float y;

		public void Set(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
	}

	public class LifeTime : IComponent
	{
		public int ticks;
	}

	public class MovementSystem : SystemBase
	{
		private float axisBound = 100;

		public override void Execute()
		{
			var entities = Context.WithAll<Position, Velocity>().GetEntities();

			foreach (var e in entities)
			{
				var v = e.Get<Velocity>();
				var pos = e.Modify<Position>();

				pos.x += v.x;
				pos.y += v.y;

				// check bound, and reflect velocity
				if ((v.x < 0 && pos.x < -axisBound) ||
					(v.x > 0 && pos.x > axisBound))
					v.x = -v.x;
				if ((v.y < 0 && pos.y < -axisBound) ||
					(v.y > 0 && pos.y > axisBound))
					v.y = -v.y;
			}
		}
	}

	public class LifeTimeSystem : SystemBase
	{
		private const int initChildCount = 1000;
		private const int minChildCount = -3;
		private const int maxChildCount = 4;
		private const int maxChildLifeTime = 1000;
		private const float maxAxisSpeed = 20;

		public override void Execute()
		{
			var entities = Context.WithAll<Position, LifeTime>().GetEntities();

			foreach (var e in entities)
			{
				var lifeTime = e.Get<LifeTime>();
				if (lifeTime.ticks-- <= 0)
				{
					var pos = e.Get<Position>();

					// spawn children and destroy self
					var random = new Random(e.id);

					var childCount = (e.id == 1) 
						? initChildCount 
						: random.Next(minChildCount, maxChildCount);

					for (int i = 0; i < childCount; i++)
						Spawn(pos.x, pos.y, random);

					e.Destroy();
				}
			}
		}

		private void Spawn(float x, float y, Random random)
		{
			var child = Context.CreateEntity();

			child.Add<LifeTime>().ticks = random.Next(1, maxChildLifeTime);
			child.Add<Position>().Set(x, y);

			float vx = ((float)random.NextDouble() - 0.5f) * maxAxisSpeed;
			float vy = ((float)random.NextDouble() - 0.5f) * maxAxisSpeed;
			child.Add<Velocity>().Set(vx, vy);
		}
	}

	public class BenchmarkCase
	{
		private Context context;
		private SystemManager systems;

		public void Init()
		{
			context = Context.Default;

			var e = context.CreateEntity();

			e.Add<Position>(); // x = y = 0, without Velocity
			e.Add<LifeTime>(); // ticks = 0

			systems = new SystemManager(context);
			systems.CollectAll();
		}

		public void Execute()
		{
			int _iterateCount = 0;

			while (context.Count > 0)
			{
				_iterateCount++;

				systems.Execute();
				//systems.Cleanup();
			}
		}

		public void Cleanup()
		{
			//systems.TearDown();
			//context.Reset();

			systems = null;
			context = null;

			//Contexts.DestroyInstance();
		}
	}

	public class Program
	{
		static void Main(string[] args)
		{
			var sw = new Stopwatch();
			var benchmark = new BenchmarkCase();

			var mem0 = GC.GetTotalMemory(false);

			sw.Start();
			benchmark.Init();
			sw.Stop();
			var initTime = sw.ElapsedMilliseconds;

			var mem1 = GC.GetTotalMemory(false);

			sw.Restart();
			benchmark.Execute();
			sw.Stop();
			var execTime = sw.ElapsedMilliseconds;

			sw.Restart();
			benchmark.Cleanup();
			sw.Stop();
			var cleanupTime = sw.ElapsedMilliseconds;

			var mem2 = GC.GetTotalMemory(false);

			Console.WriteLine($"Init = {initTime}ms, Execute = {execTime}ms, Cleanup = {cleanupTime}, Memory = {(mem2 - mem1) / 1024}KB");
		}
	}
}
