using System;
using Entitas.Utils;

namespace Entitas
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class FeatureAttruibte : Attribute
	{
		public readonly string name;
		public readonly int priority;

		public FeatureAttruibte(string name, int prior = 0)
		{
			this.name = name;
			priority = prior;
		}

		protected FeatureAttruibte(int prior = 0)
		{
			this.name = RemoveSuffix(GetType().Name);
			priority = prior;
		}

		protected static string RemoveSuffix(string name)
		{
			return name.RemoveSuffix("Feature");
		}
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class NonameFeature: FeatureAttruibte
	{
		public static readonly string NAME = "Noname";

		public NonameFeature(int prior = 0) :base(prior) {}
	}
}
