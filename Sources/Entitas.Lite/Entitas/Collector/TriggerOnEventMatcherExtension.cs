namespace Entitas {

    public static class TriggerOnEventMatcherExtension {

        public static TriggerOnEvent Added(this IMatcher matcher) {
            return new TriggerOnEvent(matcher, GroupEvent.Added);
        }

        public static TriggerOnEvent Removed(this IMatcher matcher) {
            return new TriggerOnEvent(matcher, GroupEvent.Removed);
        }

        public static TriggerOnEvent AddedOrRemoved(this IMatcher matcher) {
            return new TriggerOnEvent(matcher, GroupEvent.AddedOrRemoved);
        }
    }
}
