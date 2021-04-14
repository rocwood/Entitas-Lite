using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Entitas
{
	public abstract class EntityQuery
	{
		private readonly EntitySet _entities = new EntitySet();

		public int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _entities.Count;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Entity GetAt(int index) => _entities.GetAt(index);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IEnumerator<Entity> GetEnumerator() => _entities.GetEnumerator();


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void HandleEntityUpdate(Entity e)
		{
			if (e.isEnabled && e.IsMatch(_indices, _matchAny))
				_entities.Add(e);
			else
				_entities.Remove(e);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void InitWithEntity(Entity e)
		{
			if (e.isEnabled && e.IsMatch(_indices, _matchAny))
				_entities.Add(e);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected void make(params int[] indices) => _indices = indices;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static int inc<T>() where T:IComponent => ComponentTypeInfo<T>.index;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static int outc<T>() where T:IComponent => -ComponentTypeInfo<T>.index-1;

		protected EntityQuery(Context c, bool matchAny)
		{
			_context = c;
			_matchAny = matchAny;
		}

		protected readonly Context _context;
		protected readonly bool _matchAny;
		protected int[] _indices;
	}
}
