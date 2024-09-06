using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class MovementMechanics
    {
        private readonly IAtomicValue<bool> _isMoveEnable;
        private readonly IAtomicValue<Vector3> _moveDirection;
        private readonly IAtomicValue<float> _speed;
        private readonly Transform _transform;

        public MovementMechanics(
            IAtomicValue<bool> isMoveEnable,
            AtomicVariable<Vector3> moveDirection,
            IAtomicValue<float> speed,
            Transform transform)
        {
            _isMoveEnable = isMoveEnable;
            _moveDirection = moveDirection;
            _speed = speed;
            _transform = transform;
        }

        public void Update()
        {
            if (_isMoveEnable.Value && (_moveDirection.Value.magnitude > 0f))
            {
                Vector3 moveDirection = _moveDirection.Value.normalized;
                Vector3 moveOffset = moveDirection * _speed.Value * Time.deltaTime;
                _transform.position += moveOffset;
            }        
        }
    }
}