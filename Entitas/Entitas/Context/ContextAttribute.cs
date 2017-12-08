using System;
using Entitas.Utils;

namespace Entitas
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	public abstract class ContextAttribute : Attribute
	{
		public readonly string name;

		protected ContextAttribute()
		{
			name = RemoveSuffix(GetType().Name);
		}

		public static string GetName<C>() where C : ContextAttribute
		{
			return RemoveSuffix(typeof(C).Name);
		}

		protected static string RemoveSuffix(string name)
		{
			return name.RemoveSuffix("Context");
		}
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public sealed class Default : ContextAttribute
	{
		public const string NAME = "Default";
	}
}
