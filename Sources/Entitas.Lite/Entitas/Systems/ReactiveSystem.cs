using System.Collections.Generic;

namespace Entitas {

    /// A ReactiveSystem calls Execute(entities) if there were changes based on
    /// the specified Collector and will only pass in changed entities.
    /// A common use-case is to react to changes, e.g. a change of the position
    /// of an entity to update the gameObject.transform.position
    /// of the related gameObject.
    public abstract class ReactiveSystem : IReactiveSystem {

		readonly MonitorList _monitors;
        string _toStringCache;
		
        protected ReactiveSystem() {
            _monitors = new MonitorList();
        }

		protected ReactiveSystem(params IMonitor[] monitors) {
            _monitors = new MonitorList(monitors);
        }

		/// Use += operator only, but not real assignment
		protected MonitorList monitors { get { return _monitors; } set { } }

		protected void Add(IMonitor monitor) {
			_monitors.Add(monitor);
		}

		protected void Remove(IMonitor monitor) {
			_monitors.Remove(monitor);
		}

        /// Activates the ReactiveSystem and starts observing changes
        /// based on the specified Collector.
        /// ReactiveSystem are activated by default.
        public void Activate() {
            for (int i = 0; i < _monitors.Count; i++) {
                _monitors[i].Activate();
            }
        }

        /// Deactivates the ReactiveSystem.
        /// No changes will be tracked while deactivated.
        /// This will also clear the ReactiveSystem.
        /// ReactiveSystem are activated by default.
        public void Deactivate() {
            for (int i = 0; i < _monitors.Count; i++) {
                _monitors[i].Deactivate();
            }
        }

        /// Clears all accumulated changes.
        public void Clear() {
            for (int i = 0; i < _monitors.Count; i++) {
                _monitors[i].Clear();
            }
        }

        /// Will call Execute(entities) with changed entities
        /// if there are any. Otherwise it will not call Execute(entities).
        public virtual void Execute() {
            for (int i = 0; i < _monitors.Count; i++) {
				_monitors[i].Execute();
            }
        }

        public override string ToString() {
            if (_toStringCache == null) {
                _toStringCache = "ReactiveSystem(" + GetType().Name + ")";
            }

            return _toStringCache;
        }

        ~ReactiveSystem() {
            Deactivate();
        }
    }

}
