using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class PhysicalRotationMechanics
    {
        private readonly IAtomicValue<bool> _isRotateEnabled;
        private readonly IAtomicValue<Vector3> _lookDirection;
        private readonly IAtomicValue<float> _speed;
        private readonly Rigidbody _rigidbody;

        public PhysicalRotationMechanics(
            IAtomicValue<bool> isRotateEnabled,
            IAtomicValue<Vector3> lookDirection,
            IAtomicValue<float> speed,
            Rigidbody rigidbody)
        {
            _isRotateEnabled = isRotateEnabled;
            _lookDirection = lookDirection;
            _speed = speed;
            _rigidbody = rigidbody;
        }

        public void FixedUpdate()
        {
            if (_isRotateEnabled.Value && _lookDirection.Value != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_lookDirection.Value);

                _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation,
                    Time.fixedDeltaTime * _speed.Value));
            }
        }
    }
}