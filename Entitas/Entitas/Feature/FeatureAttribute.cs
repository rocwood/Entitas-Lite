using System;
using Entitas.Utils;

namespace Entitas
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class FeatureAttribute : Attribute
	{
		public readonly string name;
		public readonly int priority;

		public FeatureAttribute(string name, int prior = 0)
		{
			this.name = name;
			priority = prior;
		}

		protected FeatureAttribute(int prior = 0)
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
	public class NonameFeature: FeatureAttribute
	{
		public static readonly string NAME = "Noname";

		public NonameFeature(int prior = 0) :base(prior) {}
	}
}
