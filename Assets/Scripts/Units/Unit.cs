using Events;
using Plugins.SimpleEventBus;
using UnityEngine;
using UnityEngine.AI;

namespace Units
{
    public enum UnitType
    {
        Animal,
        Hunter
    }
    
    [RequireComponent(typeof(UnitClickHandler))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class Unit : MonoBehaviour
    {
        public UnitType UnitType { get; private set; }
        
        [SerializeField]
        private Renderer _renderer;
        
        private UnitStatesController _statesController;
        private TargetDetector _targetDetector;

        public void Init(UnitsSettingsProvider unitsSettings)
        {
            var navMeshAgent = GetComponent<NavMeshAgent>();
            _targetDetector = new TargetDetector();
            _statesController = new UnitStatesController(unitsSettings,_targetDetector, navMeshAgent);
            SetRandomRole();

            var clickHandler = GetComponent<UnitClickHandler>();
            clickHandler.Init(OnUnitClick);
            
            var collisionHandler = GetComponent<UnitCollisionHandler>();
            collisionHandler.Init(TryDestroyTarget);
        }

        public void InvertRole()
        {
            UnitType = UnitType == UnitType.Animal ? UnitType.Hunter : UnitType.Animal;
            ActivateUnit();
        }

        public void Destroy()
        {
            if(!gameObject.activeSelf) return;
            EventStreams.UserInterface.Publish(new UnitDestroyedEvent(UnitType));
            gameObject.SetActive(false);
        }

        private void OnUnitClick()
        {
            var nextType = UnitType == UnitType.Animal ? UnitType.Hunter : UnitType.Animal;
            EventStreams.UserInterface.Publish(new UnitRoleChanged(nextType));
            InvertRole();
        }

        private void TryDestroyTarget(Unit target)
        {
            if (UnitType == UnitType.Hunter && target.UnitType == UnitType.Animal)
            {
                DestroyTarget(target);
            }
        }

        private void DestroyTarget(Unit target)
        {
            transform.localScale += Vector3.one;
            target.Destroy();
        }

        private void SetRandomRole()
        {
            var maxRandomNumber = 100;
            var randomNumber = Random.Range(0, maxRandomNumber);
            UnitType = randomNumber > maxRandomNumber / 2 ? UnitType.Hunter : UnitType.Animal;
            ActivateUnit();
        }

        private void ActivateUnit()
        {
            _statesController.RoleChanged(UnitType);
            gameObject.layer = LayerMask.NameToLayer(UnitType.ToString());
            transform.localScale = Vector3.one; 
            _renderer.material.color = UnitType == UnitType.Hunter ? Color.red : Color.blue;
        }
        
        private void Update()
        {
            _statesController?.Update();
        }
    }
}
