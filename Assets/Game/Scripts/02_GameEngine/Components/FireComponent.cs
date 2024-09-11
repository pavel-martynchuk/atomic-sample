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
        private IAtomicValue<Weapon> _currentWeapon;
        
        public AtomicVariable<bool> FireEnable = new(true);
        
        [Get(ObjectAPI.FireAction)]
        public FireAction FireAction;
        
        public AtomicEvent FireEvent;
        
        [SerializeField, ReadOnly] private AndExpression _fireCondition;
        [SerializeField, ReadOnly] private SpawnBulletAction _spawnBulletAction;
        [SerializeField, ReadOnly] private AtomicVariable<int> _charges = new(10);
        
        public void Compose(IAtomicValue<Weapon> currentWeapon)
        {
            _currentWeapon = currentWeapon;
            
            _fireCondition.Append(FireEnable);
            _fireCondition.Append(_charges.AsFunction(it => it.Value > 0));
            
            _spawnBulletAction.Compose(_currentWeapon);
            FireAction.Compose(_spawnBulletAction, _charges, _fireCondition);
        }

        public void Dispose()
        {
            FireEvent?.Dispose();
            _charges?.Dispose();
        }
    }
}