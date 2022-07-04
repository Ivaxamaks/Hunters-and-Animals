using UnityEngine.AI;
using UnityEngine;
using Utility;
using FSM;

namespace Units.AnimalStates
{
    public class RunState : StateBase
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly TargetDetector _targetDetector;
        private readonly UnitsSettingsProvider _settings;
        private readonly NavMeshPath _navMeshPath;

        public RunState(UnitsSettingsProvider settings, TargetDetector targetDetector,
            NavMeshAgent navMeshNavMeshAgent, bool needsExitTime) : base(needsExitTime)
        {
            _settings = settings;
            _targetDetector = targetDetector;
            _navMeshAgent = navMeshNavMeshAgent;
            _navMeshPath = new NavMeshPath();
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
            if (_navMeshAgent.hasPath && _navMeshAgent.remainingDistance > _settings.MovementErrorDistance)
            {
                return;
            }
            
            var movePoint = GetRandomPatch();
            GetRandomPatch();
            _navMeshAgent.SetDestination(movePoint);

        }

        private Vector3 GetRandomPatch()
        {
            var movePoint = GetRandomPoint();
            var isSafePoint = CheckMovePoint(movePoint);
            
            if (!isSafePoint && _navMeshAgent.CalculatePath(movePoint, _navMeshPath))
            {
                return GetRandomPatch();
            }

            return movePoint;
        }

        private Vector3 GetRandomPoint()
        {
            var randomRunDistance = Utilities.RandomMinMaxVector2(_settings.AnimalMinMaxRunDistance);
            var pointX = Random.Range(-randomRunDistance, randomRunDistance);
            var pointY = Random.Range(-randomRunDistance, randomRunDistance);
            var currentPosition = _navMeshAgent.transform.position;
            return  new Vector3(pointX, 0, pointY) + currentPosition;
        }

        private bool CheckMovePoint(Vector3 movePoint)
        {
            var currentPosition = _navMeshAgent.transform.position;
            var targetPosition = _targetDetector.Target.gameObject.transform.position;
            var distanceToTarget = Vector3.Distance(currentPosition, targetPosition);
            var distancePointToTarget = Vector3.Distance( movePoint, targetPosition);
            return distanceToTarget + _settings.MovementErrorDistance < distancePointToTarget;
        }
    }
}
