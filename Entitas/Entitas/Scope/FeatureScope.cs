using System;

namespace Entitas
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class FeatureScope : Scope
	{
		public readonly string name;
		public readonly int priority;

		public FeatureScope(string name, int prior = 0) { this.name = name; priority = prior; }
	}

	public class DefaultFeature : FeatureScope
	{
		public DefaultFeature(int prior = 0) : base(Name, prior) {}

		public static readonly string Name;

		static DefaultFeature()
		{
			Name = typeof(DefaultFeature).Name;
		}
	}
}
