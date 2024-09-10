using System;
using Atomic.Elements;
using GameEngine.AtomicObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class PickupMechanics : IDisposable
    {
        [ShowInInspector, PropertyOrder(-1)] public bool IsEnable => _activePickup.Value != null;
        [ShowInInspector, ReadOnly] private bool _isProcessing = false;

        [SerializeField, ReadOnly, InlineProperty]
        private AtomicVariable<PickupObject> _activePickup = new();

        #region Public API

        public IAtomicObservable TriggerEnterEvent => _triggerEnterEvent;
        public IAtomicObservable TriggerExitEvent => _triggerExitEvent;
        public IAtomicObservable StartPickingUpEvent => _startPickingUpEvent;
        public IAtomicObservable StopPickingUpEvent => _stopPickingUpEvent;
        public IAtomicObservable PickingUpCompleteEvent => _pickingUpCompleteEvent;

        #endregion

        private AtomicEvent _triggerEnterEvent;
        private AtomicEvent _triggerExitEvent;

        private AtomicEvent _startPickingUpEvent;
        private AtomicEvent _stopPickingUpEvent;

        private AtomicEvent _pickingUpCompleteEvent;

        [SerializeField, ReadOnly] private float _duration = 0f;
        [SerializeField, ReadOnly] private float _progress = 0f;

        public void Compose()
        {
            _triggerEnterEvent = new AtomicEvent();
            _triggerExitEvent = new AtomicEvent();
            _startPickingUpEvent = new AtomicEvent();
            _stopPickingUpEvent = new AtomicEvent();
            _pickingUpCompleteEvent = new AtomicEvent();
        }

        public void OnUpdate()
        {
            PickingUpCheck();
        }

        public void OnTriggerEnter(Collider collider)
        {
            PickupObject pickupObject = collider.GetComponentInParent<PickupObject>();
            if (pickupObject != null)
            {
                FocusOn(pickupObject);
                _triggerEnterEvent.Invoke();
            }
        }

        public void OnTriggerExit(Collider collider)
        {
            PickupObject pickupObject = collider.GetComponentInParent<PickupObject>();
            if (pickupObject != null && pickupObject == _activePickup.Value)
            {
                Unfocus();
                _triggerExitEvent.Invoke();
            }
        }

        private void FocusOn(PickupObject pickupObject)
        {
            Unfocus();
            _activePickup.Value = pickupObject;
            _activePickup.Value.Select();
        }

        private void Unfocus()
        {
            _activePickup.Value?.Deselect();
            _activePickup.Value = null;
        }

        public void StartPickingUp()
        {
            _isProcessing = true;
            _startPickingUpEvent.Invoke();
        }

        public void StopPickingUp()
        {
            _isProcessing = false;
            ResetPickingUpProgress();
            _stopPickingUpEvent.Invoke();
        }

        private void PickingUpCheck()
        {
            if (!IsEnable)
                return;

            _activePickup.Value.RefreshPickupProgress(_progress);
            
            if (!_isProcessing)
                return;
            
            PickingUpInProcess();
        }

        private void PickingUpInProcess()
        {
            _duration += Time.deltaTime;
            _progress = _duration / _activePickup.Value.PickupDuration;
            if (_duration > _activePickup.Value.PickupDuration)
            {
                PickingUpComplete();
            }
        }

        private void PickingUpComplete()
        {
            _pickingUpCompleteEvent.Invoke();
        }

        private void ResetPickingUpProgress()
        {
            _duration = 0f;
            _progress = 0f;
        }

        public void Dispose()
        {
            _activePickup?.Dispose();
            _triggerEnterEvent?.Dispose();
            _triggerExitEvent?.Dispose();
            _startPickingUpEvent?.Dispose();
            _stopPickingUpEvent?.Dispose();
            _pickingUpCompleteEvent?.Dispose();
        }
    }
}