using FSM;
using UnityEngine;

namespace Units.AnimalStates
{
    public class RunState : StateBase
    {
        public RunState(bool needsExitTime) : base(needsExitTime)
        {
        }
        
        public override void OnEnter()
        {
            Debug.Log("RunState");
        }

        public override void OnLogic()
        {
        }
    }
}
