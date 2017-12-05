using System;

namespace Entitas {

    public abstract class AbstractEntityIndex<TKey> : IEntityIndex {

        public string name { get { return _name; } }

        protected readonly string _name;
        protected readonly IGroup _group;
        protected readonly Func<Entity, IComponent, TKey> _getKey;
        protected readonly Func<Entity, IComponent, TKey[]> _getKeys;
        protected readonly bool _isSingleKey;

        protected AbstractEntityIndex(string name, IGroup group, Func<Entity, IComponent, TKey> getKey) {
            _name = name;
            _group = group;
            _getKey = getKey;
            _isSingleKey = true;
        }

        protected AbstractEntityIndex(string name, IGroup group, Func<Entity, IComponent, TKey[]> getKeys) {
            _name = name;
            _group = group;
            _getKeys = getKeys;
            _isSingleKey = false;
        }

        public virtual void Activate() {
            _group.OnEntityAdded += onEntityAdded;
            _group.OnEntityRemoved += onEntityRemoved;
        }

        public virtual void Deactivate() {
            _group.OnEntityAdded -= onEntityAdded;
            _group.OnEntityRemoved -= onEntityRemoved;
            clear();
        }

        public override string ToString() {
            return name;
        }

        protected void indexEntities(IGroup group) {
            foreach (var entity in group) {
                if (_isSingleKey) {
                    addEntity(_getKey(entity, null), entity);
                } else {
                    var keys = _getKeys(entity, null);
                    for (int i = 0; i < keys.Length; i++) {
                        addEntity(keys[i], entity);
                    }
                }
            }
        }

        protected void onEntityAdded(IGroup group, Entity entity, int index, IComponent component) {
            if (_isSingleKey) {
                addEntity(_getKey(entity, component), entity);
            } else {
                var keys = _getKeys(entity, component);
                for (int i = 0; i < keys.Length; i++) {
                    addEntity(keys[i], entity);
                }
            }
        }

        protected void onEntityRemoved(IGroup group, Entity entity, int index, IComponent component) {
            if (_isSingleKey) {
                removeEntity(_getKey(entity, component), entity);
            } else {
                var keys = _getKeys(entity, component);
                for (int i = 0; i < keys.Length; i++) {
                    removeEntity(keys[i], entity);
                }
            }
        }

        protected abstract void addEntity(TKey key, Entity entity);

        protected abstract void removeEntity(TKey key, Entity entity);

        protected abstract void clear();

        ~AbstractEntityIndex() {
            Deactivate();
        }
    }
}
