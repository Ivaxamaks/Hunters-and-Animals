using Units.AnimalStates;
using Units.HunterStates;
using FSM;

namespace Units
{
    public class UnitStatesController
    {
        private readonly Unit _unit;
        private readonly StateMachine _stateMachine;

        public UnitStatesController(Unit unit)
        {
            _unit = unit;
            _stateMachine = new StateMachine();
            InitAnimalStates();
            InitHunterStates();
            _stateMachine.AddTransition(
                "HunterState",
                "AnimalState",
                transition => _unit.UnitType == UnitType.Animal);
            _stateMachine.AddTransition(
                "AnimalState",
                "HunterState",
                transition => _unit.UnitType == UnitType.Hunter);
            _stateMachine.Init();
        }

        private void InitHunterStates()
        {
            var hunterStateMachine = new StateMachine();
            _stateMachine.AddState("HunterState", hunterStateMachine);
            hunterStateMachine.AddState("WanderState",new WanderState(false));
            hunterStateMachine.AddState("ChaseState",new ChaseState(false));
            hunterStateMachine.AddTransition(
                "WanderState",
                "ChaseState",
                transition => _unit.Target != null);
            hunterStateMachine.AddTransition(
                "ChaseState",
                "WanderState",
                transition => _unit.Target == null);
        }

        private void InitAnimalStates()
        {
            var animalStateMachine = new StateMachine();
            _stateMachine.AddState("AnimalState", animalStateMachine);
            animalStateMachine.AddState("IdleState",new IdleState(false));
            animalStateMachine.AddState("RunState",new RunState(false));
            animalStateMachine.AddTransition(
                "IdleState",
                "RunState",
                transition => _unit.Target != null);
            animalStateMachine.AddTransition(
                "RunState",
                "IdleState",
                transition => _unit.Target == null);
            animalStateMachine.Init();
        }

        public void Update()
        {
            _stateMachine.OnLogic();
        }
    }
}
