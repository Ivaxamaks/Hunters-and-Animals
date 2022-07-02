using FSM;
using UnityEngine;

namespace Units.HunterStates
{
    public class ChaseState : StateBase
    {
        public ChaseState(bool needsExitTime) : base(needsExitTime)
        {
        }
        
        public override void OnEnter()
        {
            Debug.Log("ChaseState");
        }

        public override void OnLogic()
        {
        }
    }
}
