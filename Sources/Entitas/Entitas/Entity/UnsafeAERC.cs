using System.Threading;

namespace Entitas
{
	/// Automatic Entity Reference Counting (AERC)
	/// is used internally to prevent pooling retained entities.
	/// If you use retain manually you also have to
	/// release it manually at some point.
	/// UnsafeAERC doesn't check if the entity has already been
	/// retained or released. It's faster, but you lose the information
	/// about the owners.
	public sealed class UnsafeAERC : IAERC
	{
		public int retainCount => _retainCount;

		private int _retainCount;

		public void Retain(object owner)
		{
			Interlocked.Increment(ref _retainCount);
		}

		public void Release(object owner)
		{
			Interlocked.Decrement(ref _retainCount);
		}
	}
}
