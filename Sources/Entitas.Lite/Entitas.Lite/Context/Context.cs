using System.Runtime.CompilerServices;

namespace Entitas
{
	public partial class Context
	{
		public static int maxRetainedComponents = 128;

		private readonly string _contextName;
		private readonly IComponentPool[] _componentPools;

		public string name
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _contextName;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal IComponentPool[] GetComponentPools() => _componentPools;

		public Context(string contextName)
		{
			_contextName = contextName;

			var componentInfoList = ContextInfo.GetComponentInfoList();
			
			int componentCount = componentInfoList.Length;
			_componentPools = new IComponentPool[componentCount];

			for (int i = 0; i < componentCount; i++)
				_componentPools[i] = ComponentPoolFactory.Create(componentInfoList[i].type, componentInfoList[i].zeroSize, maxRetainedComponents);

			_createQueryParams = new object[] { this };
		}

		public void Poll()
		{
			if (_entitiesModified.Count <= 0)
				return;

			foreach (var e in _entitiesModified.Values)
			{
				// update all groups, TODO: optimize matching
				for (int j = 0; j < _queryList.Count; j++)
					_queryList[j].HandleEntityUpdate(e);

				if (e.isEnabled)
				{
					e.ResetModified();
				}
				else 
				{
					// remove disabled entities
					_entities.Remove(e.id);

					e.DestroyImpl();
					_entityPool.Return(e);
				}
			}

			_entitiesModified.Clear();
		}

		public void Clear()
		{
			// TODO

			ClearGroups();
		}
	}
}
