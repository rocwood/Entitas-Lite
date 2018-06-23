using System.Collections.Generic;

namespace Entitas {

	public class Monitor : IMonitor {

		readonly ICollector _collector;
		readonly List<Entity> _buffer;

		MonitorFilter _filter;
		MonitorProcessor _processor;

		public Monitor(ICollector colllector)
		{
			_collector = colllector;
			_buffer = new List<Entity>();
		}

		/// This will exclude all entities which don't pass the filter.
		public IMonitor Where(MonitorFilter filter)
		{
			_filter = filter;
			return this;
		}

		public IMonitor Trigger(MonitorProcessor processor)
		{
			_processor = processor;
			return this;
		}

		public void Activate()
		{
			_collector.Activate();
		}

		public void Deactivate()
		{
			_collector.Deactivate();
		}

		public void Clear()
		{
			_collector.ClearCollectedEntities();
		}

		public void Execute()
		{
			if (_collector.count != 0)
			{
				foreach (var e in _collector.collectedEntities)
				{
					// pass always if no filter provided 		
					if (_filter == null || _filter(e))
					{
						e.Retain(this);
						_buffer.Add(e);
					}
				}

				_collector.ClearCollectedEntities();

				if (_buffer.Count != 0)
				{
					if (_processor != null)
						_processor(_buffer);

					for (int i = 0; i < _buffer.Count; i++)
					{
						_buffer[i].Release(this);
					}
					_buffer.Clear();
				}
			}
		}

		~Monitor()
		{
			Deactivate();
		}
	}
}
