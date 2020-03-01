namespace Entitas
{
	public static class IComponentExtension
	{
		/// <summary>
		/// Set modification flag
		/// </summary>
		public static void Modify(this IComponent component)
		{
			if (component is IModifiable modifiable)
				Modify(modifiable);
		}

		/// <summary>
		/// Set modification flag
		/// </summary>
		public static void Modify(this IModifiable modifiable)
		{
			if (modifiable != null)
				modifiable.modified = true;
		}

		/// <summary>
		/// Reset modification flag
		/// </summary>
		public static void Accept(this IModifiable modifiable)
		{
			if (modifiable != null)
				modifiable.modified = false;
		}

		internal static void SetEntityId(this IComponent component, int entityId)
		{
			if (component is IEntityIdRef entityIdRef)
				entityIdRef.entityId = entityId;
		}
	}
}
