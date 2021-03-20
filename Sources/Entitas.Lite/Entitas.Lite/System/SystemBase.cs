namespace Entitas
{
	public abstract class SystemBase
	{
		public Context Context { get; internal set; }

		public abstract void Execute();
	}
}
