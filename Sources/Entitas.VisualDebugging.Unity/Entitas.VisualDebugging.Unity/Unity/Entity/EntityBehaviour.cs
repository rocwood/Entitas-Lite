using Entitas.Utils;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity {

    [ExecuteInEditMode]
    public class EntityBehaviour : MonoBehaviour {

        public IContext context { get { return _context; } }
        public IEntity entity { get { return _entity; } }

        IContext _context;
        IEntity _entity;
        string _cachedName;

        ObjectPool<EntityBehaviour> _pool;

        public void Init(IContext context, IEntity entity, ObjectPool<EntityBehaviour> pool) {
            _pool = pool;
            _context = context;
            _entity = entity;
            _entity.OnEntityReleased += onEntityReleased;

            gameObject.SetActive(true);
            Update();
        }

        void onEntityReleased(IEntity e) {
            if (_entity != null)
                _entity.OnEntityReleased -= onEntityReleased;

            gameObject.SetActive(false);

            _context = null;
            _entity = null;
            name = _cachedName = string.Empty;

            _pool.Push(this);
            _pool = null;
        }

        void Update() {
            if (_entity != null && _cachedName != _entity.ToString()) {
                name = _cachedName = _entity.ToString();
            }
        }

        void OnDestroy() {
            if (_entity != null)
                _entity.OnEntityReleased -= onEntityReleased;
        }
    }
}
