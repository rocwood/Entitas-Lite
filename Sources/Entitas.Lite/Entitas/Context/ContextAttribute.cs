using System;
using Entitas.Utils;

namespace Entitas
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	public abstract class ContextAttribute : Attribute {
		public readonly string name;

		protected ContextAttribute() {
			name = GetName(this);
		}

		public static string GetName<C>() where C : ContextAttribute {
			return ContextAttributeNameCache<C>.Name;
		}

		private static string GetName(ContextAttribute c) {
			return GetName(c.GetType());
		}

		private static string GetName(Type type) {
			return type.Name.RemoveSuffix("Attribute")
							.RemoveSuffix("Context");
		}

		private static class ContextAttributeNameCache<C> where C: ContextAttribute {
			public static readonly string Name = GetName(typeof(C));
			static ContextAttributeNameCache() {}
		}
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public sealed class Default : ContextAttribute {}
}
