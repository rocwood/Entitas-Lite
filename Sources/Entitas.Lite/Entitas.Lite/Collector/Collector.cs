using System;
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
