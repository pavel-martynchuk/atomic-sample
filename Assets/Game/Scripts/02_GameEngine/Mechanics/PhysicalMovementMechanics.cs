using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class PhysicalMovementMechanics
    {
        private readonly IAtomicValue<bool> _isMoveEnable;
        private readonly IAtomicValue<Vector3> _moveDirection;
        private readonly IAtomicValue<float> _speed;
        private readonly Rigidbody _rigidbody;

        public PhysicalMovementMechanics(
            IAtomicValue<bool> isMoveEnable,
            AtomicVariable<Vector3> moveDirection,
            IAtomicValue<float> speed,
            Rigidbody rigidbody)
        {
            _isMoveEnable = isMoveEnable;
            _moveDirection = moveDirection;
            _speed = speed;
            _rigidbody = rigidbody;
        }

        public void FixedUpdate()
        {
            if (_isMoveEnable.Value && (_moveDirection.Value.magnitude > 0f))
            {
                Vector3 moveDirection = _moveDirection.Value.normalized;
                Vector3 rigidbodyDirection = _rigidbody.transform.forward.normalized;

                float directionDot = Vector3.Dot(rigidbodyDirection, moveDirection);
                float adjustedSpeedMultiplier = Mathf.Clamp01((directionDot + 1) / 2);
                float adjustedSpeed = _speed.Value * adjustedSpeedMultiplier;

                Vector3 moveOffset = moveDirection * (adjustedSpeed * Time.fixedDeltaTime);
                _rigidbody.MovePosition(_rigidbody.position + moveOffset);
            }        
        }
        
    }
}
