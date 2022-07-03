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
        private readonly UnitsCounter _unitCounter;
        private readonly UnitsSettingsProvider _unitsSettingsProvider;

        public UnitManager(Map map, UnitsSettings unitsSettings)
        {
            var unitObject = unitsSettings.UnitPrefab.gameObject;
            _unitGenerator = new UnitGenerator(map, unitObject);
            _unitsSettingsProvider = new UnitsSettingsProvider(unitsSettings);
            _unitCounter = new UnitsCounter();
            _activeUnits = new List<Unit>();
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<CreateUnitsEvent>(CreateUnitsEventHandler),
                EventStreams.UserInterface.Subscribe<DestroyUnitsEvent>(DestroyUnitsEventHandler),
                EventStreams.UserInterface.Subscribe<InvertUnitsEvent>(InvertUnitsEventHandler),
                EventStreams.UserInterface.Subscribe<UnitDestroyedEvent>(UnitDestroyedEventHandler),
                EventStreams.UserInterface.Subscribe<UnitRoleChanged>(UnitRoleChangedEventHandler)
            };
        }

        public void Dispose()
        {
            _unitsSettingsProvider.Dispose();
            _subscriptions.Dispose();
        }

        private void UnitRoleChangedEventHandler(UnitRoleChanged eventData)
        {
            _unitCounter.UnitRoleChanged(eventData.UnitType);
        }

        private void UnitDestroyedEventHandler(UnitDestroyedEvent eventData)
        {
            _unitCounter.UnitDestroyed(eventData.type);
        }

        private void DestroyUnitsEventHandler(DestroyUnitsEvent eventData)
        {
            _activeUnits.Clear();
            _unitGenerator.UnitsToPool();
            _unitCounter.ProvideDestroyedUnitsCount();
        }

        private void CreateUnitsEventHandler(CreateUnitsEvent eventData)
        {
            var newUnits = GenerateUnits();
            ActivateUnits(newUnits);
            _activeUnits.AddRange(newUnits);
            _unitCounter.CreatedUnitsCount(newUnits);
        }

        private List<Unit> GenerateUnits()
        {
            var unitCreateAmount = _unitsSettingsProvider.GetCreateAmount();
            var newUnits = _unitGenerator.Generate(unitCreateAmount);
            return newUnits;
        }

        private void ActivateUnits(List<Unit> newUnits)
        {
            foreach (var unit in newUnits)
            {
                unit.Init(_unitsSettingsProvider);
            }
        }
        
        private void InvertUnitsEventHandler(InvertUnitsEvent eventData)
        {
            foreach (var unit in _activeUnits)
            {
                unit.InvertRole();
            }
            
            _unitCounter.InvertedUnitsCount();
        }
    }
}