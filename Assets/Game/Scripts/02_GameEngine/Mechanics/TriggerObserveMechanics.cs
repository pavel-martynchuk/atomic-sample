using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public class TriggerObserveMechanics
    {
        [SerializeField] private AtomicAction<Collider> _triggerEnterAction;
        [SerializeField] private AtomicAction<Collider> _triggerExitAction;
        
        public void OnTriggerEnter(Collider collider)
        {
            _triggerEnterAction.Invoke(collider);
        }
        
        public void OnTriggerExit(Collider collider)
        {
            _triggerExitAction.Invoke(collider);
        }
    }
}