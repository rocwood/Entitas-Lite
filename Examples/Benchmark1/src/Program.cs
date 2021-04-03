using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Entitas.Benchmark
{
	public class Position : IComponent
	{
		public float x;
		public float y;

		public void Set(float x, float y)
		{
			this.x = x;
			this.y = y;
		}
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
		public int id;
		public int ticks;

		public void Set(int id, int ticks)
		{
			this.id = id;
			this.ticks = ticks;
		}
	}

	public class MovementSystem : SystemBase
	{
		private const float axisBound = 100;

		private Group query;

		public override void Execute()
		{
			if (query == null)
				query = Context.WithAll<Position, Velocity>().GetGroup();

			//var entities = query.GetEntities();

			//foreach (var e in entities)
			for (int i = 0; i < query.Count; i++)
			{
				var e = query.GetAt(i);

				var v = e.Get<Velocity>();
				var pos = e.Get<Position>();

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

		private Group query;

		public override void Execute()
		{
			if (query == null)
				query = Context.WithAll<Position, LifeTime>().GetGroup();

			//var entities = query.GetEntities();

			//foreach (var e in entities)
			for (int i = 0; i < query.Count; i++)
			{
				var e = query.GetAt(i); 
				
				var lifeTime = e.Get<LifeTime>();
				if (lifeTime.ticks-- > 0)
					continue;

				var pos = e.Get<Position>();

				// spawn children and destroy self
				var random = new Random(lifeTime.id);

				var childCount = (lifeTime.id == 1) 
					? initChildCount 
					: random.Next(minChildCount, maxChildCount);

				for (int j = 0; j < childCount; j++)
					Spawn(pos.x, pos.y, random);

				e.Destroy();
			}
		}

		private void Spawn(float x, float y, Random random)
		{
			var child = Context.CreateEntity();

			child.Add<LifeTime>().Set(random.Next(1000, int.MaxValue), random.Next(1, maxChildLifeTime));
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

			e.Add<Position>();			// x = y = 0, without Velocity
			e.Add<LifeTime>().id = 1;	// ticks = 0

			systems = new SystemManager(context);
			systems.CollectAll();
		}

		public int frameId { get; private set; }

		public void Execute()
		{
			frameId = 0;

			while (context.Count > 0)
			{
				systems.Execute();
				//systems.Cleanup();

				frameId++;
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

			Console.WriteLine($"Frame = {benchmark.frameId}\n");
			Console.WriteLine($"Init = {initTime}ms, {(mem1 - mem0) / 1024}KB\nExec = {execTime}ms, {(mem2 - mem1) / 1024}KB\nClean = {cleanupTime}");
		}
	}
}
