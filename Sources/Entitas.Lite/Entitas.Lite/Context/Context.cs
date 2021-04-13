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

		public Context(string contextName)
		{
			_contextName = contextName;

			var componentInfoList = ContextInfo.GetComponentInfoList();
			
			int componentCount = componentInfoList.Length;
			_componentPools = new IComponentPool[componentCount];

			for (int i = 0; i < componentCount; i++)
				_componentPools[i] = ComponentPoolFactory.Create(componentInfoList[i].type, componentInfoList[i].zeroSize, maxRetainedComponents);
		}

		public void Poll()
		{
			HandleGroupChanges();

			DestroyDisabledEntities();
			ResetModifiedEntities(); 
		}

		public void Clear()
		{
			// TODO

			ClearGroups();
		}
	}
}
