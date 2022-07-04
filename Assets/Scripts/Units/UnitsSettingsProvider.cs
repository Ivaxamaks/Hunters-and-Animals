using UnityEngine;
using Events;
using SimpleEventBus.Disposables;
using Plugins.SimpleEventBus;

namespace Units
{
    public  class  UnitsSettingsProvider
    {
        public float TargetUpdateCooldown { get; private set; }
        public float MovementErrorDistance { get; private set; }
        public Vector2 HunterMinMaxWanderDistance { get; private set; }
        public Vector2 AnimalMinMaxRunDistance { get; private set; }
        
        private float _hunterDetectRadius;
        private float _animalDetectRadius;
        private int _unitCreateAmount;

        private readonly CompositeDisposable _subscriptions;


        public UnitsSettingsProvider(UnitsSettings unitsSettings)
        {
            InitBaseSettings(unitsSettings);
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<AnimalRadiusChangedEvent>(AnimalRadiusChanged),
                EventStreams.UserInterface.Subscribe<UnitCreateAmountChangedEvent>(CreateAmountChangeHandler)
            };
        }

        public float GetDetectRadius(UnitType type)
        {
            return type == UnitType.Hunter ? _hunterDetectRadius : _animalDetectRadius;
        }
        
        public int GetCreateAmount()
        {
            return _unitCreateAmount;
        }


        private void CreateAmountChangeHandler(UnitCreateAmountChangedEvent eventData)
        {
            _unitCreateAmount = eventData.Amount;
        }

        private void InitBaseSettings(UnitsSettings unitsSettings)
        {
            _animalDetectRadius = unitsSettings.AnimalDetectRadius;
            _hunterDetectRadius = unitsSettings.HunterDetectRadius;
            TargetUpdateCooldown = unitsSettings.TargetUpdateCooldown;
            MovementErrorDistance = unitsSettings.MovementErrorDistance;
            HunterMinMaxWanderDistance = unitsSettings.HunterWanderDistance;
            AnimalMinMaxRunDistance = unitsSettings.AnimalMinMaxRunDistance;
        }

        private void AnimalRadiusChanged(AnimalRadiusChangedEvent eventData)
        {
            _animalDetectRadius = eventData.Radius;
        }

        public void Dispose()
        {
            _subscriptions.Dispose();
        }
    }
}