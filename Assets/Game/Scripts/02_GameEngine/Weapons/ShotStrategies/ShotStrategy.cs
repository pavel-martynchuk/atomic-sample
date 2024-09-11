using GameEngine.Data;
using UnityEngine;

namespace GameEngine
{
    public abstract class ShotStrategy
    {
        protected readonly Projectile Projectile;
        protected readonly Transform FirePoint;
        protected readonly WeaponConfig WeaponConfig;

        protected ShotStrategy(Projectile projectile, Transform firePoint, WeaponConfig weaponConfig)
        {
            Projectile = projectile;
            FirePoint = firePoint;
            WeaponConfig = weaponConfig;
        }

        public abstract void Shot();
        
        public abstract void Move();
    }
}