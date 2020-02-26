using System;
using System.Threading.Tasks;
using Entitas.Utils;

namespace Entitas
{
	public class Collector
	{
		private readonly WeakReference<Context> _context;

		private Matcher _matcher;
		private Group _group;

		internal Collector(Context context)
		{
			_context = new WeakReference<Context>(context);
			_matcher = new Matcher();
		}

		public Entity[] GetEntities()
		{
			// Don't create group until using it
			if (_group == null)
			{
				if (!_context.TryGetTarget(out var c))
					return null;

				_group = c.GetGroup(_matcher);
			}

			return _group.GetEntities();
		}

		public void ForEach(Action<Entity> body)
		{
			if (body == null)
				return;

			var entities = _group.GetEntities();

			foreach (var entity in entities)
				body.Invoke(entity);
		}

		public void ParallelForEach(Action<Entity> body)
		{
			if (body == null)
				return;

			var entities = _group.GetEntities();

			Parallel.ForEach(entities, body);
		}

		public Collector AllOf(BitArray mask)
		{
			_matcher.AllOf(mask);
			_group = null;
			return this;
		}

		public Collector AnyOf(BitArray mask)
		{
			_matcher.AnyOf(mask);
			_group = null;
			return this;
		}

		public Collector NoneOf(BitArray mask)
		{
			_matcher.NoneOf(mask);
			_group = null;
			return this;
		}
	}
}
