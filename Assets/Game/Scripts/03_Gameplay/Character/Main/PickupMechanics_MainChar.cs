using System;
using Atomic.Elements;
using GameEngine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Character.Main
{
    // ReSharper disable once InconsistentNaming
    [Serializable]
    public sealed class PickupMechanics_MainChar : IDisposable
    {
        [InlineProperty, PropertyOrder(0)]
        public AtomicVariable<bool> MechanicsEnable = new(true);
        
        [ShowInInspector, PropertyOrder(-1)]
        private bool ResultCondition => MechanicsEnable.Value && (_pickup.Value != null);

        public PickupAction PickupAction;
        public AtomicEvent TriggerEnter;
        public AtomicEvent TriggerExit;
        
        [SerializeField, InlineProperty]
        private TriggerObserver _triggerObserver;
        
        [SerializeField, ReadOnly, InlineProperty]
        private AtomicVariable<PickupObject> _pickup = new();
        
        [SerializeField, ReadOnly, InlineProperty]
        private AtomicVariable<bool> _inProcessing = new();
        
        [SerializeField, ReadOnly] private float _time = 0f;
        [SerializeField, ReadOnly] private float _progress = 0f;

        public void Compose()
        {
            PickupAction.Compose();
        }
        
        public void OnUpdate()
        {
            TryToProcessing();
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
                TriggerEnter?.Invoke();
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
                TriggerExit?.Invoke();
                pickupObject.Deselect();
                _pickup.Value = null;
            }
        }
        
        public void StartProcessing()
        {
            if (!ResultCondition)
                return;
            _inProcessing.Value = true;
        }

        public void EndProcessing()
        {
            if (_inProcessing.Value)
            {
                ResetProgress();
                _inProcessing.Value = false;
            }
        }

        private void TryToProcessing()
        {
            if (ResultCondition && _inProcessing.Value)
            {
                _time += Time.deltaTime;
                _progress = _time / _pickup.Value.PickupDuration;
                _pickup.Value.RefreshPickupProgress(_progress);
                if (_time > _pickup.Value.PickupDuration)
                {
                    Complete();
                }
            }
        }

        private void Complete()
        {
            ResetProgress();
            _inProcessing.Value = false;
            _pickup.Value.Use();
            _pickup.Value = null;
            PickupAction.Invoke();
        }

        private void ResetProgress()
        {
            _time = 0f;
        }

        public void Dispose()
        {
            MechanicsEnable?.Dispose();
            PickupAction?.Dispose();
            _pickup?.Dispose();
            _inProcessing?.Dispose();
        }
    }
}