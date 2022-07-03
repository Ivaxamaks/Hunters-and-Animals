using SimpleEventBus.Events;
using Units;

namespace Events
{
    public class UnitRoleChanged : EventBase 
    {
        public UnitType UnitType { get; }

        public UnitRoleChanged(UnitType unitType)
        {
            UnitType = unitType;
        }
    }
}