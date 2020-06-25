using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.ObjectPool;
using UnityEngine;

namespace Entitas.VisualDebugging.Unity
{
	public class ContextObserver
	{
		public IContext context { get { return _context; } }
		public IGroup[] groups { get { return _groups.ToArray(); } }
		public GameObject gameObject { get { return _gameObject; } }

		readonly IContext _context;
		readonly List<IGroup> _groups;
		readonly GameObject _gameObject;

		readonly ObjectPool<EntityBehaviour> _pool;

		StringBuilder _toStringBuilder = new StringBuilder();

		public ContextObserver(IContext context)
		{
			_context = context;
			_groups = new List<IGroup>();
			_gameObject = new GameObject();
			_gameObject.AddComponent<ContextObserverBehaviour>().Init(this);

			_pool = CreatePool(_gameObject.transform);

			_context.OnEntityCreated += onEntityCreated;
			_context.OnGroupCreated += onGroupCreated;
		}

		public void Deactivate()
		{
			_context.OnEntityCreated -= onEntityCreated;
			_context.OnGroupCreated -= onGroupCreated;
		}

		void onEntityCreated(IContext context, IEntity entity)
		{
			var entityBehaviour = _pool.Get();
			entityBehaviour.Init(context, entity, _pool);
		}

		void onGroupCreated(IContext context, IGroup group)
		{
			_groups.Add(group);
		}

		public override string ToString()
		{
			_toStringBuilder.Length = 0;
			_toStringBuilder
				.Append("[Context] ")
				.Append(_context.contextInfo.name).Append(" (")
				.Append(_context.count).Append(" entities, ")
			//	.Append(_context.reusableEntitiesCount).Append(" reusable, ");
			//	.Append(_context.retainedEntitiesCount).Append(" retained, ");
				.Append(_groups.Count).Append(" groups)");

			var str = _toStringBuilder.ToString();
			_gameObject.name = str;
			return str;
		}

		EntityBehaviour CreateEntityBehaviour()
		{
			var entityBehaviour = new GameObject().AddComponent<EntityBehaviour>();
			entityBehaviour.transform.SetParent(_gameObject.transform, false);
			return entityBehaviour;
		}

		static ObjectPool<EntityBehaviour> CreatePool(Transform root)
		{
			var provider = new DefaultObjectPoolProvider();
			var policy = new PoolPolicy(root);

			return provider.Create(policy);
		}

		class PoolPolicy : IPooledObjectPolicy<EntityBehaviour>
		{
			private Transform _root;

			public PoolPolicy(Transform root)
			{
				_root = root;
			}

			public EntityBehaviour Create()
			{
				var behaviour = new GameObject().AddComponent<EntityBehaviour>();
				behaviour.transform.SetParent(_root, false);

				return behaviour;
			}

			public bool Return(EntityBehaviour obj)
			{
				if (obj == null)
					return false;

				obj.gameObject.SetActive(false);
				return true;
			}
		}
	}
}
