using UnityEngine.AI;
using FSM;

namespace Units.AnimalStates
{
    public class RunState : StateBase
    {
        private readonly NavMeshAgent _navMeshAgent;
        
        public RunState(NavMeshAgent navMeshNavMeshAgent, bool needsExitTime) : base(needsExitTime)
        {
            _navMeshAgent = navMeshNavMeshAgent;
        }
        
        public override void OnEnter()
        {
            _navMeshAgent.isStopped = false;
        }

        public override void OnExit()
        {
            _navMeshAgent.ResetPath();
            _navMeshAgent.isStopped = true;
        }

        public override void OnLogic()
        {
        }
    }
}
