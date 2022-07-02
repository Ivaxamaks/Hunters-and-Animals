using System.Collections.Generic;
using Events;
using MapGeneration;
using Plugins.SimpleEventBus;
using SimpleEventBus.Disposables;

namespace Units
{
    public class UnitManager
    {
        private readonly List<Unit> _activeUnits;
        private readonly UnitGenerator _unitGenerator;
        private readonly CompositeDisposable _subscriptions;

        private int _unitCreateAmount;

        public UnitManager(Map map, UnitsSettings unitsSettings)
        {
            var unitObject = unitsSettings.UnitPrefab.gameObject;
            _unitGenerator = new UnitGenerator(map, unitObject);
            _activeUnits = new List<Unit>();
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<CreateUnitsEvent>(CreateEventHandler),
                EventStreams.UserInterface.Subscribe<UnitCreateAmountChange>(CreateAmountChangeHandler),
                EventStreams.UserInterface.Subscribe<DestroyUnitsEvent>(DestroyUnitsEventHandler),
                EventStreams.UserInterface.Subscribe<InvertUnitsEvent>(InvertUnitsEventHandler),
            };
        }

        public void Dispose()
        {
            _subscriptions.Dispose();
        }
        
        private void DestroyUnitsEventHandler(DestroyUnitsEvent eventData)
        {
            _activeUnits.Clear();
            _unitGenerator.UnitsToPool();
        }

        private void CreateEventHandler(CreateUnitsEvent obj)
        {
            var newUnits = _unitGenerator.Generate(_unitCreateAmount);
            _activeUnits.AddRange(newUnits);
        }

        private void CreateAmountChangeHandler(UnitCreateAmountChange eventData)
        {
            _unitCreateAmount = eventData.Amount;
        }

        private void InvertUnitsEventHandler(InvertUnitsEvent eventData)
        {
            foreach (var unit in _activeUnits)
            {
                unit.InvertRole();
            }
        }
    }
}