using NUnit.Framework;
using Entitas;

namespace Entitas.Test
{
	class TestCheckEmptyComponent
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Test1()
		{
			Assert.IsFalse(CheckEmptyComponent.IsEmpty<Position>());
			Assert.IsFalse(CheckEmptyComponent.IsEmpty<Player>());
			Assert.IsTrue(CheckEmptyComponent.IsEmpty<Movable>());
			Assert.IsFalse(CheckEmptyComponent.IsEmpty<Dummy>());
			Assert.IsFalse(CheckEmptyComponent.IsEmpty<Dummy3>());
			Assert.IsFalse(CheckEmptyComponent.IsEmpty<Dummy4>());
			Assert.IsFalse(CheckEmptyComponent.IsEmpty<Dummy5>());
		}

		class Position : IComponent
		{
			public float x, y, z;
		}

		class Player : IComponent
		{
			public ulong id;
			public string name;
			public int level;
		}

		class Movable : IComponent
		{
		}

		class Dummy : ComponentWithEntityID
		{
		}

		class Dummy2 : IComponent
		{
			private int _val;
		}

		class Dummy3 : Dummy2
		{
		}

		class Dummy4 : IComponent
		{
			public int id { get; set; }
		}

		class Dummy5 : IComponent
		{
			public void print() { }
		}
	}
}
