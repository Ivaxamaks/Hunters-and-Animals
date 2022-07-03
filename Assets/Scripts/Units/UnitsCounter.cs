using System.Collections.Generic;
using Plugins.SimpleEventBus;
using Events;

namespace Units
{
    public class UnitsCounter
    {
        private int _huntersCount;
        private int _animalsCount;

        public void CreatedUnitsCount(List<Unit> newUnits)
        {
            foreach (var unit in newUnits)
            {
                if (unit.UnitType == UnitType.Animal)
                {
                    _animalsCount++;
                    continue;
                }

                _huntersCount++;
            }
            
            PublishCount();
        }

        public void UnitRoleChanged(UnitType type)
        {
            if (type == UnitType.Animal)
            {
                _animalsCount++;
                _huntersCount--;
            }
            else
            {
                _huntersCount++;
                _animalsCount--; 
            }
            
            PublishCount();
        }
        
        public void UnitDestroyed(UnitType type)
        {
            if (type == UnitType.Animal)
            {
                _animalsCount--;
            }
            else
            {
                _huntersCount--;
            }

            PublishCount();
        }
        
        public void ProvideDestroyedUnitsCount()
        {
            _huntersCount = 0;
            _animalsCount = 0;
            PublishCount();
        }
        
        public void InvertedUnitsCount()
        {
            (_animalsCount, _huntersCount) = (_huntersCount, _animalsCount);
            PublishCount();
        }

        private void PublishCount()
        {
            EventStreams.UserInterface.Publish(new ActiveUnitsCountChangedEvent(_huntersCount, _animalsCount));
        }
    }
}