namespace Entitas
{
	static class ContextStringExtension
	{
		const string ContextSuffix = "Context";

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
	}
}
