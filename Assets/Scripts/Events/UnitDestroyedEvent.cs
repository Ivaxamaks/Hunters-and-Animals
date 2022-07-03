using SimpleEventBus.Events;
using Units;

namespace Events
{
    public class UnitDestroyedEvent : EventBase 
    {
        public UnitType type { get; }

        public UnitDestroyedEvent(UnitType type)
        {
            this.type = type;
        }
    }
}