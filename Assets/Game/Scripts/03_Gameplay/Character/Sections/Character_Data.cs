using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.StaticData;
using GameEngine;
using GameEngine.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Character
{
    // ReSharper disable once InconsistentNaming
    [Serializable]
    public sealed class Character_Data : IDisposable
    {
        [ReadOnly, InlineProperty]
        public AtomicVariable<int> Health;
        
        [InlineProperty]
        [PropertySpace(SpaceBefore = 24, SpaceAfter = 24)]
        [Get(ObjectAPI.SpeedStat)]
        public Stat MovementSpeed;
        
        [ReadOnly, InlineProperty]
        public AtomicVariable<float> RotationSpeed;
        
        [Space(20f)]
        [Header("Const")]
        [ReadOnly, InlineProperty] public AtomicValue<float> AcceleratedSpeed;
        [ReadOnly, InlineProperty] public AtomicValue<float> DashDistance;
        [ReadOnly, InlineProperty] public AtomicValue<float> DashDuration;
        
        public void Compose(CharacterStaticData staticDataConfig)
        {
            MovementSpeed.Compose(staticDataConfig.MovementSpeed);
            
            Health = new AtomicVariable<int>(staticDataConfig.Health);
            RotationSpeed = new AtomicVariable<float>(staticDataConfig.RotationSpeed);

            AcceleratedSpeed = new AtomicValue<float>(staticDataConfig.AcceleratedSpeed);
            DashDistance = new AtomicValue<float>(staticDataConfig.DashDistance);
            DashDuration = new AtomicValue<float>(staticDataConfig.DashDuration);
        }
        
        public void Dispose()
        {
            Health?.Dispose();
            MovementSpeed?.Dispose();
            RotationSpeed?.Dispose();
        }
    }
}