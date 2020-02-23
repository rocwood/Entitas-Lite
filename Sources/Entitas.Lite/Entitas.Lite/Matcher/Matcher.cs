using Entitas.Utils;

namespace Entitas
{
	public partial class Matcher
	{
		internal BitArray combineMask
		{
			get {

				if (_combineMask == null)
				{
					_combineMask = CreateMask();
					_combineMask.Union(_allOfMask);
					_combineMask.Union(_anyOfMask);
					_combineMask.Union(_noneOfMask);
				}

				return _combineMask;
			}
		}

		internal BitArray allOfMask => _allOfMask;
		internal BitArray anyOfMask => _anyOfMask;
		internal BitArray noneOfMask => _noneOfMask;

		BitArray _combineMask;
		BitArray _allOfMask;
		BitArray _anyOfMask;
		BitArray _noneOfMask;

		public Matcher AllOf(params int[] indices) => AllOf(CreateMask(indices));
		public Matcher AnyOf(params int[] indices) => AnyOf(CreateMask(indices));
		public Matcher NoneOf(params int[] indices) => NoneOf(CreateMask(indices));
		private BitArray CreateMask(int[] indices = null) => new BitArray(ContextProvider.GetComponentCount(), indices);

		public Matcher AllOf(BitArray mask)
		{
			_allOfMask = mask;
			_combineMask = null;
			_isHashCached = false;
			return this;
		}

		public Matcher AnyOf(BitArray mask)
		{
			_anyOfMask = mask;
			_combineMask = null;
			_isHashCached = false;
			return this;
		}

		public Matcher NoneOf(BitArray mask)
		{
			_noneOfMask = mask;
			_combineMask = null;
			_isHashCached = false;
			return this;
		}

		public bool Matches(Entity entity)
		{
			return (_allOfMask == null || entity.HasAllComponents(_allOfMask))
				&& (_anyOfMask == null || entity.HasAnyComponent(_anyOfMask))
				&& (_noneOfMask == null || !entity.HasAnyComponent(_noneOfMask));
		}
	}
}
