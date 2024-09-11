using System.Collections.Generic;
using System.Linq;
using GameEngine.Data;
using UnityEngine;

namespace GameEngine
{
    public class Buckshot : ShotStrategy
    {
        private readonly List<Projectile> _projectiles = new();

        public Buckshot(Projectile projectile, Transform firePoint, WeaponConfig weaponConfig) : base(projectile, firePoint, weaponConfig)
        {
        }

        public override void Shot()
        {
            for (int i = 0; i < 12; i++)
            {
                Projectile projectileInstance = Object.Instantiate(Projectile, FirePoint.position, FirePoint.rotation);
                _projectiles.Add(projectileInstance);

                Vector3 spreadDirection = Quaternion.Euler(0, Random.Range(-5f, 5f), 0) * FirePoint.forward;

                projectileInstance.transform.forward = spreadDirection;
            }
        }

        public override void Move()
        {
            foreach (Projectile projectile in _projectiles.Where(projectile => projectile != null))
            {
                projectile.transform.Translate(projectile.transform.forward * WeaponConfig.ProjectileSpeed * Time.deltaTime);

                if (Vector3.Distance(projectile.transform.position, FirePoint.position) >= WeaponConfig.MaxRange)
                {
                    Object.Destroy(projectile.gameObject);
                }
            }
        }
    }
}
