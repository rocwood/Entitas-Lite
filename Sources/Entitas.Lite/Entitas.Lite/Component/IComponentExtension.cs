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
		/// Accept modification flag, then reset
		/// </summary>
		public static void Commit(this IModifiable modifiable)
		{
			if (modifiable != null)
				modifiable.modified = false;
		}
	}
}
