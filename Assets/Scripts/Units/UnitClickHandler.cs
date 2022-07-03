using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace Units
{
    public sealed class UnitClickHandler : MonoBehaviour, IPointerClickHandler
    {
        private Action _action;

        public void Init(Action action)
        {
            _action = action;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _action.Invoke();
        }
    }
}