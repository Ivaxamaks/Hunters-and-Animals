using SimpleEventBus.Events;

namespace Events
{
    public class UnitCreateAmountChange : EventBase
    {
        public int Amount { get; }

        public UnitCreateAmountChange(int amount)
        {
            Amount = amount;
        }
    }
}