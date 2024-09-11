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
        private IAtomicValue<Weapon> _currentWeapon;

        public void Compose(IAtomicValue<Weapon> currentWeapon)
        {
            _currentWeapon = currentWeapon;
        }

        public void Invoke()
        {
            AtomicObject bullet = Object.Instantiate(
                _currentWeapon.Value.Projectile,
                _currentWeapon.Value.FirePoint.position,
                _currentWeapon.Value.FirePoint.rotation,
                null
            );
            
            IAtomicVariable<Vector3> bulletDirection = bullet.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            
            if (bulletDirection != null)
            {
                bulletDirection.Value = _currentWeapon.Value.FirePoint.forward;
            }
        }
    }
}