using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class FireAction : AtomicEvent
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
        public override void Invoke()
        {
            if (!_shootCondition.Value)
                return;

            if (_weapon.Value.HasAmmo.Value)
            {
                _weapon.Value.ShotStrategy.Shot();
            }
            else
            {
                Debug.LogWarning("No ammo!");
            }
            base.Invoke();
        }
    }
}