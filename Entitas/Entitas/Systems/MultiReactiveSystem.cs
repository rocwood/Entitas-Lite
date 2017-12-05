using System.Collections.Generic;

namespace Entitas {

    /// A ReactiveSystem calls Execute(entities) if there were changes based on
    /// the specified Collector and will only pass in changed entities.
    /// A common use-case is to react to changes, e.g. a change of the position
    /// of an entity to update the gameObject.transform.position
    /// of the related gameObject.
    public abstract class MultiReactiveSystem : IReactiveSystem {

        readonly ICollector[] _collectors;
        readonly List<Entity> _buffer;
        string _toStringCache;

        protected MultiReactiveSystem(IContexts contexts) {
            _collectors = GetTrigger(contexts);
            _buffer = new List<Entity>();
        }

        protected MultiReactiveSystem(ICollector[] collectors) {
            _collectors = collectors;
            _buffer = new List<Entity>();
        }

        /// Specify the collector that will trigger the ReactiveSystem.
        protected abstract ICollector[] GetTrigger(IContexts contexts);

        /// This will exclude all entities which don't pass the filter.
        protected abstract bool Filter(Entity entity);

        protected abstract void Execute(List<Entity> entities);

        /// Activates the ReactiveSystem and starts observing changes
        /// based on the specified Collector.
        /// ReactiveSystem are activated by default.
        public void Activate() {
            for (int i = 0; i < _collectors.Length; i++) {
                _collectors[i].Activate();
            }
        }

        /// Deactivates the ReactiveSystem.
        /// No changes will be tracked while deactivated.
        /// This will also clear the ReactiveSystem.
        /// ReactiveSystem are activated by default.
        public void Deactivate() {
            for (int i = 0; i < _collectors.Length; i++) {
                _collectors[i].Deactivate();
            }
        }

        /// Clears all accumulated changes.
        public void Clear() {
            for (int i = 0; i < _collectors.Length; i++) {
                _collectors[i].ClearCollectedEntities();
            }
        }

        /// Will call Execute(entities) with changed entities
        /// if there are any. Otherwise it will not call Execute(entities).
        public void Execute() {
            for (int i = 0; i < _collectors.Length; i++) {
                var collector = _collectors[i];
                if (collector.count != 0) {
                    foreach (var e in collector.GetCollectedEntities()) {
                        if (Filter(e)) {
                            e.Retain(this);
                            _buffer.Add(e);
                        }
                    }

                    collector.ClearCollectedEntities();
                }
            }

            if (_buffer.Count != 0) {
                Execute(_buffer);
                for (int i = 0; i < _buffer.Count; i++) {
                    _buffer[i].Release(this);
                }
                _buffer.Clear();
            }
        }

        public override string ToString() {
            if (_toStringCache == null) {
                _toStringCache = "MultiReactiveSystem(" + GetType().Name + ")";
            }

            return _toStringCache;
        }

        ~MultiReactiveSystem() {
            Deactivate();
        }
    }
}
