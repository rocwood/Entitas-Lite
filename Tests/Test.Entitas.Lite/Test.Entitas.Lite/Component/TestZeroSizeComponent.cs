using NUnit.Framework;

namespace Entitas.Test
{
	[TestFixture]
	public class TestZeroSizeComponent
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Test1()
		{
			Assert.IsFalse(ComponentTrait.IsZeroSize<Position>());
			Assert.IsFalse(ComponentTrait.IsZeroSize<Player>());
			Assert.IsTrue(ComponentTrait.IsZeroSize<Movable>());
			Assert.IsFalse(ComponentTrait.IsZeroSize<Dummy>());
			Assert.IsFalse(ComponentTrait.IsZeroSize<Dummy3>());
			Assert.IsFalse(ComponentTrait.IsZeroSize<Dummy4>());
			Assert.IsFalse(ComponentTrait.IsZeroSize<Dummy5>());
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

		class Dummy : IComponent, IEntityIdRef
		{
			public int entityId { get; set; }
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
