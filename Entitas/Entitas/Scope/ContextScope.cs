using System;

namespace Entitas
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public abstract class ContextScope : Scope
	{
	}

	public class Default : ContextScope
	{
	}
}
