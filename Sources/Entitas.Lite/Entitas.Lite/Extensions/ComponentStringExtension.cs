namespace Entitas
{
	static class ComponentStringExtension
	{
		const string ComponentSuffix = "Component";

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
	}
}
