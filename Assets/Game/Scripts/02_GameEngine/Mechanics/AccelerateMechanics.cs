using System;
using Atomic.Elements;
using GameEngine.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class AccelerateMechanics
    {
        [SerializeField, InlineProperty, ReadOnly]
        private Stat _speed;
        
        [SerializeField, InlineProperty, ReadOnly]
        private AtomicValue<float> _changedSpeed;
        
        [SerializeField, ReadOnly]
        private float _baseSpeed;

        private Guid modifier;
        
        public void Compose(Stat speed, AtomicValue<float> changedSpeed)
        {
            _speed = speed;
            _changedSpeed = changedSpeed;
        }
        
        public void ChangeSpeed()
        {
            _speed.AddAdditiveModifier(5f);
        }
        
        public void RevertSpeed()
        {
            _speed.RemoveAdditiveModifier(5f);
        }
    }
}