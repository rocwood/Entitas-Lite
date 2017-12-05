namespace Entitas {

    public static class GroupExtension {

        /// Creates a Collector for this group.
        public static ICollector CreateCollector(this IGroup group, GroupEvent groupEvent = GroupEvent.Added) {
            return new Collector(group, groupEvent);
        }
    }
}
