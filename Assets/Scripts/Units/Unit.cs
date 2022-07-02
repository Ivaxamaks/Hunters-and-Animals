using UnityEngine.AI;
using UnityEngine;

namespace Units
{
    public enum UnitType
    {
        Animal,
        Hunter
    }
    
    [RequireComponent(typeof(NavMeshAgent))]
    public class Unit : MonoBehaviour
    {
        public UnitType UnitType { get; private set; }
        public Unit Target { get; private set; }
        
        private UnitStatesController _statesController;
        
        void Start()
        {
            _statesController = new UnitStatesController(this);
        }
        
        private void Update()
        { 
            _statesController.Update();
        }

        public void InvertRole()
        {
            UnitType = UnitType == UnitType.Animal ? UnitType.Hunter : UnitType.Animal;
        }
    }
}
