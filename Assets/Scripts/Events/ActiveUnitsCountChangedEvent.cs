using SimpleEventBus.Events;

namespace Events
{
    public class ActiveUnitsCountChangedEvent : EventBase
    {
        public int ActiveHunters { get; }
        public int ActiveAnimals { get; }

        public ActiveUnitsCountChangedEvent(int activeHunters, int activeAnimals)
        {
            ActiveHunters = activeHunters;
            ActiveAnimals = activeAnimals;
        }
    }
}