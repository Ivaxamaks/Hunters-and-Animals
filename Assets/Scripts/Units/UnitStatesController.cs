using Units.AnimalStates;
using Units.HunterStates;
using UnityEngine.AI;
using FSM;

namespace Units
{
    public class UnitStatesController
    {
        private readonly UnitsSettingsProvider _settings;
        private readonly TargetDetector _targetDetector;
        private readonly NavMeshAgent _navMeshAgent;

        private  StateMachine _stateMachine;
        private UnitType _currentType;

        public UnitStatesController(UnitsSettingsProvider settings, TargetDetector targetDetector,
            NavMeshAgent navMeshAgent)
        {
            _settings = settings;
            _targetDetector = targetDetector;
            _navMeshAgent = navMeshAgent;
            InitStateMachine();
        }

        public void RoleChanged(UnitType unitType)
        {
            _currentType = unitType;
        }

        private void InitStateMachine()
        {
            _stateMachine = new StateMachine();
            InitAnimalStates();
            InitHunterStates();
            _stateMachine.AddTransition(
                "HunterState",
                "AnimalState",
                transition => _currentType == UnitType.Animal);
            _stateMachine.AddTransition(
                "AnimalState",
                "HunterState",
                transition => _currentType == UnitType.Hunter);
            _stateMachine.Init();
        }

        private void InitHunterStates()
        {
            var hunterStateMachine = new StateMachine();
            _stateMachine.AddState("HunterState", hunterStateMachine);
            hunterStateMachine.AddState("WanderState",new WanderState(_settings, _navMeshAgent, false));
            hunterStateMachine.AddState("ChaseState",new ChaseState(_targetDetector, _navMeshAgent, false));
            hunterStateMachine.AddTransition(
                "WanderState",
                "ChaseState",
                transition => _targetDetector.Target != null);
            hunterStateMachine.AddTransition(
                "ChaseState",
                "WanderState",
                transition => _targetDetector.Target == null);
        }

        private void InitAnimalStates()
        {
            var animalStateMachine = new StateMachine();
            _stateMachine.AddState("AnimalState", animalStateMachine);
            animalStateMachine.AddState("IdleState",new IdleState(false));
            animalStateMachine.AddState("RunState",new RunState(_settings, _targetDetector, _navMeshAgent, false));
            animalStateMachine.AddTransition(
                "IdleState",
                "RunState",
                transition => _targetDetector.Target != null); 
            animalStateMachine.AddTransition(
                "RunState",
                "IdleState",
                transition => _targetDetector.Target == null);
            animalStateMachine.Init();
        }

        public void Update()
        {
            _stateMachine.OnLogic();
            _targetDetector.DetectTarget(_settings, _currentType, _navMeshAgent.transform.position);
        }
    }
}
