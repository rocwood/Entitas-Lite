namespace Entitas {

    public struct TriggerOnEvent {

        public readonly IMatcher matcher;
        public readonly GroupEvent groupEvent;

        public TriggerOnEvent(IMatcher matcher, GroupEvent groupEvent) {
            this.matcher = matcher;
            this.groupEvent = groupEvent;
        }
    }
}
