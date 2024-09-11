using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class FireAction : IAtomicAction
    {
        private IAtomicValue<bool> _shootCondition;
        
        [ShowInInspector]
        private IAtomicVariable<Weapon> _weapon;

        public void Compose(IAtomicValue<bool> shootCondition, IAtomicVariable<Weapon> weapon)
        {
            _shootCondition = shootCondition;
            _weapon = weapon;
        }

        [Button]
        public void Invoke()
        {
            if (!_shootCondition.Value)
                return;
            
            _weapon.Value.ShotStrategy.Shot();
        }
    }
}