using System;

namespace Entitas
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public abstract class Scope : Attribute
	{
	}

	public static class ScopeHelper
	{
		public static string GetName(Scope scope)
		{
			return scope.GetType().Name;
		}

		public static string GetName<S>() where S : Scope
		{
			return typeof(S).Name;
		}
	}
}
