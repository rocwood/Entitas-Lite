using System.Collections.Generic;

namespace Entitas {

	public class MonitorList : List<IMonitor> {

		public MonitorList() {}
		public MonitorList(int capacity) : base(capacity) {}
		public MonitorList(IEnumerable<IMonitor> collection) : base(collection) { }

		public static MonitorList operator +(MonitorList c, IMonitor monitor) {
			c.Add(monitor);
			return c;
		}

		public static MonitorList operator -(MonitorList c, IMonitor monitor) {
			c.Remove(monitor);
			return c;
		}
	}
}
