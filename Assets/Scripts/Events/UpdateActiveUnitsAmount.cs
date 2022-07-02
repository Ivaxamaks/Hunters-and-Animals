using SimpleEventBus.Events;

namespace Events
{
    public class UpdateActiveUnitsAmount : EventBase
    {
        public int ActiveHunters { get; }
        public int ActiveAnimals { get; }

        public UpdateActiveUnitsAmount(int activeHunters, int activeAnimals)
        {
            ActiveHunters = activeHunters;
            ActiveAnimals = activeAnimals;
        }
    }
}