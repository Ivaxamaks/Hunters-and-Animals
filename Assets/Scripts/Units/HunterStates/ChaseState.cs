using FSM;
using UnityEngine.AI;

namespace Units.HunterStates
{
    public class ChaseState : StateBase
    {
        private readonly TargetDetector _targetDetector;
        private readonly NavMeshAgent _navMeshAgent;

        public ChaseState(TargetDetector targetDetector, NavMeshAgent navMeshAgent, bool needsExitTime) : base(needsExitTime)
        {
            _targetDetector = targetDetector;
            _navMeshAgent = navMeshAgent;
        }
        
        public override void OnEnter()
        {
            _navMeshAgent.ResetPath();
        }
        
        public override void OnLogic()
        {
            var targetPosition = _targetDetector.Target.gameObject.transform.position;
            _navMeshAgent.SetDestination(targetPosition);
        }
    }
}
