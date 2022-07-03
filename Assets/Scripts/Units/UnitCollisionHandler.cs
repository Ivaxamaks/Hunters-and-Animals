using UnityEngine;
using System;

namespace Units
{
    public class UnitCollisionHandler : MonoBehaviour
    {
        private Action<Unit> _action;
        
        public void Init(Action<Unit> action)
        {
            _action = action;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Unit")) return;
            var target = other.gameObject.GetComponent<Unit>();
            _action.Invoke(target);
        }
    }
}
