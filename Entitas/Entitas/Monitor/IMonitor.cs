using System.Collections.Generic;

namespace Entitas {

	public delegate bool MonitorFilter(Entity entity);
	public delegate void MonitorProcessor(List<Entity> entities);

	public interface IMonitor {

		IMonitor Where(MonitorFilter filter);
		IMonitor Trigger(MonitorProcessor processor);

		void Activate();
		void Deactivate();

		void Clear();

		void Execute();
	}
}
