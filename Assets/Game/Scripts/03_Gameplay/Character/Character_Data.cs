using System;
using Atomic.Elements;
using Game.Scripts.StaticData;
using GameEngine.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts._03_Gameplay.Character
{
    // ReSharper disable once InconsistentNaming
    [Serializable]
    public sealed class Character_Data : IDisposable
    {
        [ReadOnly, InlineProperty]
        public AtomicVariable<int> Health;
        
        [PropertySpace(SpaceBefore = 24, SpaceAfter = 24)]
        [InlineProperty]
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
            Health = new AtomicVariable<int>(staticDataConfig.Health);
            MovementSpeed = new Stat(staticDataConfig.MovementSpeed);
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