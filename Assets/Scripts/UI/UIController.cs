using UnityEngine;
using UnityEngine.UI;
using SimpleEventBus.Disposables;
using Plugins.SimpleEventBus;
using Events;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private Button _createButton;
        [SerializeField]
        private Button _destroyButton;
        [SerializeField]
        private Button _inversionButton;
        [SerializeField]
        private Button _exitButton;
        [SerializeField]
        private InputField _unitAmountField;
        [SerializeField]
        private InputField _animalRadiusField;
        [SerializeField]
        private Text _unitCounterField;
        
        private CompositeDisposable _subscriptions;

        private void Start()
        {
            _createButton.onClick.AddListener(OnCreateClick);
            _destroyButton.onClick.AddListener(OnDestroyClick);
            _inversionButton.onClick.AddListener(OnInversionClick);
            _exitButton.onClick.AddListener(OnExitClick);
            _unitAmountField.onEndEdit.AddListener(OnCreateUnitAmountChange);
            _animalRadiusField.onEndEdit.AddListener(OnAnimalRadiusChange);
            
            _subscriptions = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<ActiveUnitsCountChangedEvent>(UnitCountChangeHandler)
            };
        }

        private void UnitCountChangeHandler(ActiveUnitsCountChangedEvent eventData)
        {
            _unitCounterField.text = $" Количество Охотников: {eventData.ActiveHunters}, Количество Дичи: {eventData.ActiveAnimals}";
        }
        
        private void OnCreateClick()
        {
            EventStreams.UserInterface.Publish(new CreateUnitsEvent());
        }
        
        private void OnDestroyClick()
        {
            EventStreams.UserInterface.Publish(new DestroyUnitsEvent());
        }
        
        private void OnInversionClick()
        {
            EventStreams.UserInterface.Publish(new InvertUnitsEvent());
        }
        
        private void OnCreateUnitAmountChange(string value)
        {
            var isNumber = int.TryParse(value, out var number);
            if (isNumber)
            {
                EventStreams.UserInterface.Publish(new UnitCreateAmountChangedEvent(number)); 
            }
            else
            {
                _unitAmountField.text = "Введите числовое занчение";
            }
        }
        
        private void OnAnimalRadiusChange(string value)
        {
            var isNumber = float.TryParse(value, out var number);
            if (isNumber)
            {
                EventStreams.UserInterface.Publish(new AnimalRadiusChangedEvent(number)); 
            }
            else
            {
                _animalRadiusField.text = "Введите числовое занчение";
            }
        }
        
        private void OnExitClick()
        {
            EventStreams.UserInterface.Publish(new ExitGameEvent());
        }
        
        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }
    }
}
