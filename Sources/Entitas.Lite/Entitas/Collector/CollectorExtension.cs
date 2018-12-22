namespace Entitas {

	public static class CollectorExtension {

		/// Creates a Monitor for this collector.
		public static IMonitor CreateMonitor(this ICollector collector) {
			return new Monitor(collector);
		}

		public static IMonitor Where(this ICollector collector, MonitorFilter filter) {
			return new Monitor(collector).Where(filter);
		}

		public static IMonitor Trigger(this ICollector collector, MonitorProcessor processor) {
			return new Monitor(collector).Trigger(processor);
		}
	}
}
