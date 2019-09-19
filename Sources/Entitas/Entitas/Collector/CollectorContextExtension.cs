using System;

namespace Entitas {

    public static class CollectorContextExtension {

        /// Creates a Collector.
        // TODO Obsolete since 0.42.0, April 2017
        [Obsolete("Please use context.CreateCollector(Matcher.Xyz.Added()) (or .Removed(), or .AddedOrRemoved())")]
        public static ICollector CreateCollector(
            this IContext context, IMatcher matcher, GroupEvent groupEvent) {

            return context.CreateCollector(new TriggerOnEvent(matcher, groupEvent));
        }

        /// Creates a Collector.
        public static ICollector CreateCollector(
            this IContext context, IMatcher matcher) {

            return context.CreateCollector(new TriggerOnEvent(matcher, GroupEvent.Added));
        }

        /// Creates a Collector.
        public static ICollector CreateCollector(
            this IContext context, params TriggerOnEvent[] triggers) {

            var groups = new IGroup[triggers.Length];
            var groupEvents = new GroupEvent[triggers.Length];

            for (int i = 0; i < triggers.Length; i++) {
                groups[i] = context.GetGroup(triggers[i].matcher);
                groupEvents[i] = triggers[i].groupEvent;
            }

            return new Collector(groups, groupEvents);
        }
    }
}
