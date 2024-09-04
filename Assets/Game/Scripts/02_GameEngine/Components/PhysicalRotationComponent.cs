using System;
using Atomic.Elements;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class PhysicalRotationComponent
    {
        [InlineProperty, ReadOnly]
        public AtomicVariable<bool> RotateEnable = new(true);

        [Get(ObjectAPI.RotateDirection)]
        [SerializeField, InlineProperty, ReadOnly]
        private AtomicVariable<Vector3> _moveDirection;

        [SerializeField, InlineProperty, ReadOnly]
        private AtomicVariable<float> _speed;

        private PhysicalRotationMechanics _rotationMechanics;

        public void Compose(Rigidbody rigidbody, AtomicVariable<float> speed)
        {
            _speed = speed;
            _rotationMechanics = new PhysicalRotationMechanics(RotateEnable, _moveDirection, _speed, rigidbody);
        }

        public void FixedUpdate()
        {
            _rotationMechanics.FixedUpdate();
        }
    }
}