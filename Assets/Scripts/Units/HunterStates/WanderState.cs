using FSM;
using UnityEngine;

namespace Units.HunterStates
{
    public class WanderState : StateBase
    {
        public WanderState(bool needsExitTime) : base(needsExitTime)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("WanderState");
        }

        public override void OnLogic()
        {
        }
    }
}
