using System;
using Atomic.Elements;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public class MovementComponent
    {
        [InlineProperty, ReadOnly]
        public AtomicVariable<bool> MoveEnable = new(true);
        [InlineProperty, SerializeField]
        public AtomicFunction<bool> IsMoving;
        
        [Header("Data")]
        [Get(ObjectAPI.MoveDirection)]
        [SerializeField, InlineProperty, ReadOnly]
        private AtomicVariable<Vector3> _moveDirection;
        
        [SerializeField, InlineProperty, ReadOnly]
        private AtomicVariable<float> _speed;
        
        [Header("Mechanics")]
        private MovementMechanics _movementMechanics;
        
        public void Compose(Transform transform, AtomicVariable<float> speed)
        {
            _speed = speed;
            IsMoving.Compose(IsMovingFunc);
            _movementMechanics = new MovementMechanics(MoveEnable, _moveDirection, _speed, transform);
        }

        public void OnUpdate()
        {
            _movementMechanics.Update();
        }

        public void Dispose()
        {
            MoveEnable?.Dispose();
            _moveDirection?.Dispose();
            _speed?.Dispose();
        }

        private bool IsMovingFunc() => 
            MoveEnable.Value && (_moveDirection.Value.magnitude > 0f);
    }
}