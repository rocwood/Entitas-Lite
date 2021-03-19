namespace Entitas
{
	static class StringNameUtils
	{
		const string ComponentSuffix = "Component";
		const string ContextSuffix = "Context";
		const string SystemSuffix = "System";

		public static string AddComponentSuffix(this string componentName)
		{
			return componentName.EndsWith(ComponentSuffix, System.StringComparison.Ordinal)
								? componentName
								: componentName + ComponentSuffix;
		}

		public static string RemoveComponentSuffix(this string componentName)
		{
			return componentName.EndsWith(ComponentSuffix, System.StringComparison.Ordinal)
								? componentName.Substring(0, componentName.Length - ComponentSuffix.Length)
								: componentName;
		}

		public static string AddContextSuffix(this string contextName)
		{
			return contextName.EndsWith(ContextSuffix, System.StringComparison.Ordinal)
							  ? contextName
							  : contextName + ContextSuffix;
		}

		public static string RemoveContextSuffix(this string contextName)
		{
			return contextName.EndsWith(ContextSuffix, System.StringComparison.Ordinal)
							  ? contextName.Substring(0, contextName.Length - ContextSuffix.Length)
							  : contextName;
		}

		public static string AddSystemSuffix(this string systemName)
		{
			return systemName.EndsWith(SystemSuffix, System.StringComparison.Ordinal)
							? systemName
							: systemName + SystemSuffix;
		}

		public static string RemoveSystemSuffix(this string systemName)
		{
			return systemName.EndsWith(SystemSuffix, System.StringComparison.Ordinal)
							? systemName.Substring(0, systemName.Length - SystemSuffix.Length)
							: systemName;
		}
	}
}
