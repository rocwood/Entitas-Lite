namespace Entitas {

    public partial class Matcher : IAllOfMatcher {

        public int[] indices {
            get {
                if (_indices == null) {
                    _indices = mergeIndices(_allOfIndices, _anyOfIndices, _noneOfIndices);
                }
                return _indices;
            }
        }

        public int[] allOfIndices => _allOfIndices;
        public int[] anyOfIndices => _anyOfIndices;
        public int[] noneOfIndices => _noneOfIndices;

		public string[] componentNames => ContextProvider.GetComponentNames();

		int[] _indices;
        int[] _allOfIndices;
        int[] _anyOfIndices;
        int[] _noneOfIndices;

        Matcher() {}

        IAnyOfMatcher IAllOfMatcher.AnyOf(params int[] indices) {
            _anyOfIndices = distinctIndices(indices);
            _indices = null;
            _isHashCached = false;
            return this;
        }

        IAnyOfMatcher IAllOfMatcher.AnyOf(params IMatcher[] matchers) {
            return ((IAllOfMatcher)this).AnyOf(mergeIndices(matchers));
        }

        public INoneOfMatcher NoneOf(params int[] indices) {
            _noneOfIndices = distinctIndices(indices);
            _indices = null;
            _isHashCached = false;
            return this;
        }

        public INoneOfMatcher NoneOf(params IMatcher[] matchers) {
            return NoneOf(mergeIndices(matchers));
        }

        public bool Matches(Entity entity) {
            return (_allOfIndices == null || entity.HasComponents(_allOfIndices))
                && (_anyOfIndices == null || entity.HasAnyComponent(_anyOfIndices))
                && (_noneOfIndices == null || !entity.HasAnyComponent(_noneOfIndices));
        }
    }
}
