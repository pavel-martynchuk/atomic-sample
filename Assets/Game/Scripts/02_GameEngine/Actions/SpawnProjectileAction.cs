using System;
using Atomic.Elements;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine
{
    [Serializable]
    public sealed class SpawnProjectileAction : IAtomicAction
    {
        private IAtomicValue<Weapon> _currentWeapon;

        public void Compose(IAtomicValue<Weapon> currentWeapon)
        {
            _currentWeapon = currentWeapon;
        }

        public void Invoke()
        {
            GameObject projectile = Object.Instantiate(
                _currentWeapon.Value.Projectile,
                _currentWeapon.Value.FirePoint.position,
                _currentWeapon.Value.FirePoint.rotation,
                null
            );
        }
    }
}