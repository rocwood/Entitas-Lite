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
					_combineMask.CopyFrom(_allOfMask);
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

		public Matcher AllOf(params int[] indices)
		{
			_allOfMask = CreateMask(indices);

			_combineMask = null;
			_isHashCached = false;
			return this;
		}

		public Matcher AnyOf(params int[] indices)
		{
			_anyOfMask = CreateMask(indices);

			_combineMask = null;
			_isHashCached = false;
			return this;
		}

		public Matcher NoneOf(params int[] indices)
		{
			_noneOfMask = CreateMask(indices);

			_combineMask = null;
			_isHashCached = false;
			return this;
		}

		private BitArray CreateMask(int[] indices = null)
		{
			return new BitArray(ContextProvider.GetComponentCount(), indices);
		}

		public bool Matches(Entity entity)
		{
			return (_allOfMask == null || entity.HasAllComponents(_allOfMask))
				&& (_anyOfMask == null || entity.HasAnyComponent(_anyOfMask))
				&& (_noneOfMask == null || !entity.HasAnyComponent(_noneOfMask));
		}
	}
}
