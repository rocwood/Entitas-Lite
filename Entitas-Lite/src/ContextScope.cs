/*
 *	Entitas-Lite is a helper extension of Entitas(ECS framework for c# and Unity).
 *	Entitas-Lite focusses on easy development WITHOUT CodeGenerator of original Entitas.
 *	https://github.com/rocwood/Entitas-Lite
 */

using System;


namespace Entitas
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public abstract class ContextScope : Attribute
	{
	}

	public class DefaultContext : ContextScope
	{
		public static readonly string Name;

		static DefaultContext()
		{
			Name = ContextScopeHelper.GetName<DefaultContext>();
		}
	}


	public static class ContextScopeHelper
	{
		public static string GetName(Type scope)
		{
			return scope.Name;
		}

		public static string GetName<S>() where S : ContextScope
		{
			return typeof(S).Name;
		}
	}
}
