namespace Entitas {

    public static class GroupExtension {

        /// Creates a Collector for this group.
        public static ICollector CreateCollector(this IGroup group, GroupEvent groupEvent = GroupEvent.Added) {
            return new Collector(group, groupEvent);
        }

		public static ICollector OnAdded(this IGroup group) {
			return new Collector(group, GroupEvent.Added);
		}

		public static ICollector OnRemoved(this IGroup group) {
			return new Collector(group, GroupEvent.Removed);
		}

		public static ICollector OnAddedOrRemoved(this IGroup group) {
			return new Collector(group, GroupEvent.AddedOrRemoved);
		}

		public static IMonitor OnAdded(this IGroup group, MonitorProcessor processor) {
			return new Collector(group, GroupEvent.Added).Trigger(processor);
		}

		public static IMonitor OnRemoved(this IGroup group, MonitorProcessor processor) {
			return new Collector(group, GroupEvent.Removed).Trigger(processor);
		}

		public static IMonitor OnAddedOrRemoved(this IGroup group, MonitorProcessor processor) {
			return new Collector(group, GroupEvent.AddedOrRemoved).Trigger(processor);
		}
	}
}
