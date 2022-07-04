using UnityEngine;
using UnityEngine.AI;
using Utility;
using FSM;

namespace Units.HunterStates
{
    public class WanderState : StateBase
    {
        private readonly UnitsSettingsProvider _settings;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly NavMeshPath _navMeshPath;

        public WanderState(UnitsSettingsProvider settings, NavMeshAgent navMeshAgent, bool needsExitTime) : base(needsExitTime)
        {
            _settings = settings;
            _navMeshAgent = navMeshAgent;
            _navMeshPath = new NavMeshPath();
        }

        public override void OnEnter()
        {
            _navMeshAgent.ResetPath();
        }

        public override void OnLogic()
        {
            if (_navMeshAgent.hasPath && _navMeshAgent.remainingDistance > _settings.MovementErrorDistance)
            {
                return;
            }
            
            var movePoint = GetRandomPoint();
            if (_navMeshAgent.CalculatePath(movePoint, _navMeshPath))
            {
                movePoint = GetRandomPoint();
            }
            
            _navMeshAgent.SetDestination(movePoint);
        }
        
        private Vector3 GetRandomPoint()
        {
            var position = _navMeshAgent.gameObject.transform.position;
            var wanderRadius = Utilities.RandomMinMaxVector2(-_settings.HunterMinMaxWanderDistance);
            var movePoint = new Vector3(Random.Range(-wanderRadius, wanderRadius), 0, 
                    Random.Range(-wanderRadius, wanderRadius)) + position;
            return movePoint;
        }
    }
}
