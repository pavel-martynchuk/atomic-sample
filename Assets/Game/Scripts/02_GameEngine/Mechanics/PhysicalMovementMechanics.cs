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
            if (_isMoveEnable.Value)
            {
                Vector3 moveOffset = _moveDirection.Value * (_speed.Value * Time.fixedDeltaTime);
                _rigidbody.MovePosition(_rigidbody.position + moveOffset);
            }        
        }
        
    }
}
