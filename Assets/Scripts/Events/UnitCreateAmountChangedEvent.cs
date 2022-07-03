using SimpleEventBus.Events;

namespace Events
{
    public class UnitCreateAmountChangedEvent : EventBase
    {
        public int Amount { get; }

        public UnitCreateAmountChangedEvent(int amount)
        {
            Amount = amount;
        }
    }
}