using System;
using Atomic.Elements;
using GameEngine.AtomicObjects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class PickupMechanics : IDisposable
    {
        public AtomicVariable<PickupObject> PickupObject => _pickupObject;
        public AtomicEvent<Collider> TriggerEnterEvent => _triggerEnterAction;
        public AtomicEvent<Collider> TriggerExitEvent => _triggerExitAction;
        public AtomicEvent StartPickupEvent => _startPickupEvent;
        public AtomicEvent StopPickupEvent => _stopPickupEvent;
        public AtomicAction PickupAction => _pickupAction;
        private AtomicVariable<PickupObject> _pickupObject;

        private AtomicEvent<Collider> _triggerEnterEvent;
        private AtomicEvent<Collider> _triggerExitEvent;
        
        private AtomicEvent _startPickupEvent;
        private AtomicEvent _stopPickupEvent;
        
        private AtomicAction _pickupAction;

        public void Compose()
        {
            
        }
        
        public void OnUpdate()
        {
            
        }
        
        public void OnTriggerEnter(Collider collider)
        {
            _triggerEnterAction.Invoke(collider);
        }
        
        public void OnTriggerExit(Collider collider)
        {
            _triggerExitAction.Invoke(collider);
        }

        public void Dispose()
        {
            _pickupObject?.Dispose();
            _startPickupEvent?.Dispose();
            _stopPickupEvent?.Dispose();
        }
    }
}