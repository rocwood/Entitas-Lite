using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Entitas
{
	public class Group
	{
		private readonly Matcher _matcher;
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

		internal Group(Matcher matcher)
		{
			_matcher = matcher;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void HandleEntity(Entity entity)
		{
			if (entity == null)
				return;

			if (entity.isEnabled && _matcher.Matches(entity))
				_entities.Add(entity);
			else
				_entities.Remove(entity);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IReadOnlyList<Entity> GetEntities() => _entities.GetEntities();

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetEntities(IList<Entity> output)
		{
			output.Clear();

			for (int i = 0; i < _entities.Count; i++)
				output.Add(_entities.GetAt(i));
		}

		public override string ToString()
		{
			if (_toStringCache == null)
				_toStringCache = $"Group({_matcher})";

			return _toStringCache;
		}

		private string _toStringCache;
	}
}
