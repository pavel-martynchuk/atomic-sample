using System;
using Atomic.Elements;
using Game.Scripts.StaticData;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts._03_Gameplay.Character
{
    // ReSharper disable once InconsistentNaming
    [Serializable]
    public sealed class Character_Data : IDisposable
    {
        [Header("Reactive")]
        [SerializeField, ReadOnly, InlineProperty] private AtomicVariable<int> _health;
        [SerializeField, ReadOnly, InlineProperty] private AtomicVariable<float> _movementSpeed;
        [SerializeField, ReadOnly, InlineProperty] private AtomicVariable<float> _rotationSpeed;
        
        [Space(20f)]
        [Header("Const")]
        [SerializeField, ReadOnly, InlineProperty] private AtomicValue<float> _dashDistance;
        [SerializeField, ReadOnly, InlineProperty] private AtomicValue<float> _dashDuration;

        #region Property
        
        public AtomicVariable<int> Health => _health;
        public AtomicVariable<float> MovementSpeed => _movementSpeed;
        public AtomicVariable<float> RotationSpeed => _rotationSpeed;
        public AtomicValue<float> DashDistance => _dashDistance;
        public AtomicValue<float> DashDuration => _dashDuration;
        
        #endregion
        
        public void Compose(CharacterStaticData staticDataConfig)
        {
            _health = new AtomicVariable<int>(staticDataConfig.Health);
            _movementSpeed = new AtomicVariable<float>(staticDataConfig.MovementSpeed);
            _rotationSpeed = new AtomicVariable<float>(staticDataConfig.RotationSpeed);

            _dashDistance = new AtomicValue<float>(staticDataConfig.DashDistance);
            _dashDuration = new AtomicValue<float>(staticDataConfig.DashDuration);
        }

        public void Dispose()
        {
            _health?.Dispose();
            _movementSpeed?.Dispose();
            _rotationSpeed?.Dispose();
        }
    }
}