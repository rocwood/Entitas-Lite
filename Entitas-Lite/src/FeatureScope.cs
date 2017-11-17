/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */

using System;


namespace Entitas
{
	[AttributeUsage(AttributeTargets.Class)]
	public class FeatureScope : Attribute
	{
		public readonly string name;
		public readonly int priority;

		public FeatureScope(string name, int prior = 0) { this.name = name; priority = prior; }
	}

	[AttributeUsage(AttributeTargets.Class)]
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
