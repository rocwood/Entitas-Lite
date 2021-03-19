namespace Entitas
{
	static class SystemStringExtension
	{
		const string SystemSuffix = "System";

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
