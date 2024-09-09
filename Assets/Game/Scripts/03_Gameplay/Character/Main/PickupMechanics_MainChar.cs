using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Character.Main
{
    // ReSharper disable once InconsistentNaming
    [Serializable]
    public sealed class PickupMechanics_MainChar
    {
        [SerializeField, InlineProperty]
        private TriggerObserver _triggerObserver;
        
        [InlineProperty]
        private AtomicVariable<PickupObject> _pickup = new();
        
        public void Compose()
        {
            
        }

        public void OnEnable()
        {
            _triggerObserver.TriggerEnter += OnTriggerEnter;
            _triggerObserver.TriggerExit += OnTriggerExit;
        }

        public void OnDisable()
        {
            _triggerObserver.TriggerEnter += OnTriggerEnter;
            _triggerObserver.TriggerExit += OnTriggerExit;
        }

        private void OnTriggerEnter(Collider obj)
        {
            PickupObject pickupObject = obj.GetComponentInParent<PickupObject>();
            if (pickupObject != null)
            {
                _pickup.Value?.Deselect();
                _pickup.Value = pickupObject;
                pickupObject.Select();
            }
        }
        
        private void OnTriggerExit(Collider obj)
        {
            PickupObject pickupObject = obj.GetComponentInParent<PickupObject>();
            if (pickupObject != null && (pickupObject == _pickup.Value))
            {
                pickupObject.Deselect();
            }
        }
    }
}