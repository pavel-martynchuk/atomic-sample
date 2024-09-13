using System.Collections.Generic;
using Atomic.Elements;
using GameEngine.Data;
using UnityEngine;

namespace GameEngine
{
    public abstract class ShotStrategy
    {
        public readonly AtomicEvent OnShot = new();

        protected readonly List<GameObject> ProjectileInstances = new();
        protected readonly GameObject Projectile;
        protected readonly Transform FirePoint;
        protected readonly WeaponConfig WeaponConfig;

        protected ShotStrategy(GameObject projectile, Transform firePoint, WeaponConfig weaponConfig)
        {
            Projectile = projectile;
            FirePoint = firePoint;
            WeaponConfig = weaponConfig;
        }

        public abstract void Shot();

        public abstract void Move();

        protected void RemoveProjectile(GameObject projectile)
        {
            ProjectileInstances.Remove(projectile);
            Object.Destroy(projectile);
        }
    }
}