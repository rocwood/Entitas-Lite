namespace Entitas
{
	/// <summary>
	/// Interface for components with owner entity's ID
	/// </summary>
	public interface IComponentWithEntityID
	{
		int EntityID { get; }
	}

	/// <summary>
	/// Base component class with owner entity's ID automatic injected
	/// </summary>
	public abstract class ComponentWithEntityID : IComponentWithEntityID
	{
		private volatile int _entityID;

		public int EntityID 
		{
			get => _entityID;			// Reads from volatile int32
			set => _entityID = value;	// Writes to int32 are atomic. No need for Interlocked.Exchange(ref _entityID, value);
		}
	}
}
