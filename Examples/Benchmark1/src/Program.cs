using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

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

		public override string ToString() => $"Position({x},{y})";
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

		public override string ToString() => $"Velocity({x},{y})";
	}

	public class LifeTime : IComponent
	{
		public int ticks;

		public override string ToString() => $"LifeTime({ticks})";
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

		public int frameId { get; private set; }

		public void Execute()
		{
			frameId = 0;

			while (context.Count > 0)
			{
				//Dump();

				systems.Execute();
				//systems.Cleanup();

				frameId++;
			}
		}

		/*
		private StringBuilder _dumpBuffer = new StringBuilder();
		public string GetDumpResult() => _dumpBuffer.ToString();

		private List<Entity> tempEntitiesList = new List<Entity>();

		private void Dump()
		{
			_dumpBuffer.Append($"Frame {frameId}\n");

			context.GetEntities(tempEntitiesList);
			for (int i = 0; i < tempEntitiesList.Count; i++)
			{
				var e = tempEntitiesList[i];

				_dumpBuffer.Append(e);

				int compCount = Context.GetComponentCount();
				for (int j = 0; j < compCount; j++)
				{
					var comp = e.GetComponent(j);
					if (comp == null)
						continue;

					_dumpBuffer.Append(comp);
					_dumpBuffer.Append(' ');
				}

				_dumpBuffer.Append('\n');
			}

			_dumpBuffer.Append('\n');

			tempEntitiesList.Clear();
		}
		*/

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

			//File.WriteAllText("DumpResult.txt", benchmark.GetDumpResult());
		}
	}
}
