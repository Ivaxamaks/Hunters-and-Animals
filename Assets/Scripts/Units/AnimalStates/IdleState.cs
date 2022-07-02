using FSM;
using UnityEngine;

namespace Units.AnimalStates
{
    public class IdleState : StateBase
    {
        public IdleState(bool needsExitTime) : base(needsExitTime)
        {
        }
        
        public override void OnEnter()
        {
            Debug.Log("IdleState");
        }

        public override void OnLogic()
        {
        }
    }
}
