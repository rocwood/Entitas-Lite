namespace Entitas
{
	/// <summary>
	/// Base class with owner entity's ID automatic injected
	/// </summary>
	public abstract class ComponentWithEntityID
	{
		public int EntityID { get; internal set; }
	}
}
