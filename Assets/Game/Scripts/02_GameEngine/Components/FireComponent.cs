using System;
using Atomic.Elements;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class FireComponent : IDisposable
    {
        public AtomicVariable<bool> FireEnable = new(true);
        
        [SerializeField]
        private AtomicVariable<Weapon> _currentWeapon;

        [Get(ObjectAPI.FireAction)]
        public FireAction FireAction;

        public AtomicEvent FireEvent;
        
        [SerializeField, ReadOnly] private AndExpression _fireCondition;
        
        public void Compose(AtomicVariable<Weapon> currentWeapon)
        {
            _currentWeapon = currentWeapon;
            _fireCondition.Append(FireEnable);
            //_fireCondition.Append(_currentWeapon.Value != null);
            //_fireCondition.Append(_currentWeapon.Value); // ammo have
            FireAction.Compose(_fireCondition, _currentWeapon.Value.ShotStrategy);
        }

        public void OnUpdate()
        {
            _currentWeapon.Value.ShotStrategy.Move();
        }
        
        public void Dispose()
        {
            FireEnable?.Dispose();
            FireEvent?.Dispose();
        }
    }
}