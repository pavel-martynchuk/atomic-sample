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
        
        [Get(ObjectAPI.FireAction)]
        public FireAction FireAction;
        
        public AtomicEvent FireEvent;
        
        [SerializeField, ReadOnly] private Transform _firePoint;
        [SerializeField, ReadOnly] private AtomicObject _bulletPrefab;
        [SerializeField, ReadOnly] private AndExpression _fireCondition;
        [SerializeField, ReadOnly] private SpawnBulletAction _spawnBulletAction;
        [SerializeField, ReadOnly] private AtomicVariable<int> _charges = new(10);
        
        public void Compose(Transform firePoint , AtomicObject bulletPrefab)
        {
            _firePoint = firePoint;
            _bulletPrefab = bulletPrefab;

            _fireCondition.Append(FireEnable);
            _fireCondition.Append(_charges.AsFunction(it => it.Value > 0));
            
            _spawnBulletAction.Compose(_firePoint, _bulletPrefab);
            FireAction.Compose(_spawnBulletAction, _charges, _fireCondition);
        }

        public void Dispose()
        {
            FireEvent?.Dispose();
            _charges?.Dispose();
        }
    }
}