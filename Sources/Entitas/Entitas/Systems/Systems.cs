using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Entitas {

    /// Systems provide a convenient way to group systems.
    /// You can add IInitializeSystem, IExecuteSystem, ICleanupSystem,
    /// ITearDownSystem, ReactiveS
    /// ystem and other nested Systems instances.
    /// All systems will be initialized and executed based on the order
    /// you added them.
    public class Systems : IInitializeSystem, IExecuteSystem, ICleanupSystem, ITearDownSystem {

        protected readonly List<IInitializeSystem> _initializeSystems;
        protected readonly List<IExecuteSystem> _executeSystems;
        protected readonly List<ICleanupSystem> _cleanupSystems;
        protected readonly List<ITearDownSystem> _tearDownSystems;

		protected readonly List<ISystem> _allSystems;

		/// Creates a new Systems instance.
		public Systems() {
            _initializeSystems = new List<IInitializeSystem>();
            _executeSystems = new List<IExecuteSystem>();
            _cleanupSystems = new List<ICleanupSystem>();
            _tearDownSystems = new List<ITearDownSystem>();
			_allSystems = new List<ISystem>();
		}

        /// Adds the system instance to the systems list.
        public virtual Systems Add(ISystem system) {
            var initializeSystem = system as IInitializeSystem;
            if (initializeSystem != null) {
                _initializeSystems.Add(initializeSystem);
            }

            var executeSystem = system as IExecuteSystem;
            if (executeSystem != null) {
                _executeSystems.Add(executeSystem);
            }

            var cleanupSystem = system as ICleanupSystem;
            if (cleanupSystem != null) {
                _cleanupSystems.Add(cleanupSystem);
            }

            var tearDownSystem = system as ITearDownSystem;
            if (tearDownSystem != null) {
                _tearDownSystems.Add(tearDownSystem);
            }

			_allSystems.Add(system);
            return this;
        }

        /// Calls Initialize() on all IInitializeSystem and other
        /// nested Systems instances in the order you added them.
        public virtual void Initialize() {
            for (int i = 0; i < _initializeSystems.Count; i++) {
                _initializeSystems[i].Initialize();
            }
        }

        /// Calls Execute() on all IExecuteSystem and other
        /// nested Systems instances in the order you added them.
        public virtual void Execute() {
            for (int i = 0; i < _executeSystems.Count; i++) {
                _executeSystems[i].Execute();
            }
        }

        /// Calls Cleanup() on all ICleanupSystem and other
        /// nested Systems instances in the order you added them.
        public virtual void Cleanup() {
            for (int i = 0; i < _cleanupSystems.Count; i++) {
                _cleanupSystems[i].Cleanup();
            }
        }

        /// Calls TearDown() on all ITearDownSystem  and other
        /// nested Systems instances in the order you added them.
        public virtual void TearDown() {
            for (int i = 0; i < _tearDownSystems.Count; i++) {
                _tearDownSystems[i].TearDown();
            }
        }

		public ReadOnlyCollection<ISystem> GetAllSystems() {
			return _allSystems.AsReadOnly();
		}
	}
}
