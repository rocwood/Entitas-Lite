
namespace Entitas
{
	/// Simple Systems with automatic collections. Nested is not supported
	public class Feature : Systems
	{
		/// Constructor, name could be empty/null for noname systems
		public Feature(string name)
		{
			if (string.IsNullOrEmpty(name))
				name = UnnamedFeature.NAME;

			FeatureHelper.CollectSystems(name, this);
		}
	}
}
