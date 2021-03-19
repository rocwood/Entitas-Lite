namespace Entitas
{
	public interface IUnique
	{
	}

	public interface IEntityIdRef
	{
		int entityId { get; set; }
	}

	public interface IResetable
	{
		void Reset();
	}

	public interface IModifiable
	{
		bool modified { get; set; }
	}

	public static class IComponentExtension
	{
		public static void Modify(this IComponent component)
		{
			SetModified(component, true);
		}

		public static void SetModified(this IComponent component, bool modified)
		{
			if (component is IModifiable modifiable)
				modifiable.modified = modified;
		}
	}
}
