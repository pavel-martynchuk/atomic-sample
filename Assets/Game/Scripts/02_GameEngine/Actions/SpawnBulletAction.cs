using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine
{
    [Serializable]
    public sealed class SpawnBulletAction : IAtomicAction
    {
        private Transform _firePoint;
        private AtomicObject _bulletPrefab;

        public SpawnBulletAction(Transform firePoint, AtomicObject bulletPrefab)
        {
            _firePoint = firePoint;
            _bulletPrefab = bulletPrefab;
        }

        public void Compose(Transform firePoint, AtomicObject bulletPrefab)
        {
            _firePoint = firePoint;
            _bulletPrefab = bulletPrefab;
        }

        public void Invoke()
        {
            AtomicObject bullet = Object.Instantiate(
                _bulletPrefab,
                _firePoint.position,
                _firePoint.rotation,
                null
            );
            
            IAtomicVariable<Vector3> bulletDirection = bullet.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            
            if (bulletDirection != null)
            {
                bulletDirection.Value = _firePoint.forward;
            }
        }
    }
}