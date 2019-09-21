namespace Entitas
{
	/// <summary>
	/// Implement IComponent to provide a component which can be added to entity.
	/// </summary>
	public interface IComponent { }

	/// <summary>
	/// Components implement IUnique must not created more then once
	/// </summary>
	public interface IUnique { }
}

