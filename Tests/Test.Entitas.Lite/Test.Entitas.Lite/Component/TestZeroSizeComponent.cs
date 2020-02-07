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
			Assert.IsFalse(ComponentChecker.IsZeroSize<Position>());
			Assert.IsFalse(ComponentChecker.IsZeroSize<Player>());
			Assert.IsTrue(ComponentChecker.IsZeroSize<Movable>());
			Assert.IsFalse(ComponentChecker.IsZeroSize<Dummy>());
			Assert.IsFalse(ComponentChecker.IsZeroSize<Dummy3>());
			Assert.IsFalse(ComponentChecker.IsZeroSize<Dummy4>());
			Assert.IsFalse(ComponentChecker.IsZeroSize<Dummy5>());
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
