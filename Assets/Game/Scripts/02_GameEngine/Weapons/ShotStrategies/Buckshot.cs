using System.Collections.Generic;
using GameEngine.Data;
using UnityEngine;

namespace GameEngine
{
    public class Buckshot : ShotStrategy
    {
        private const int ProjectilesCount = 12;
        private const float SpreadAngle = 10f;

        public Buckshot(GameObject projectile, Transform firePoint, WeaponConfig weaponConfig)
            : base(projectile, firePoint, weaponConfig) { }

        public override void Shot()
        {
            for (int i = 0; i < ProjectilesCount; i++)
            {
                GameObject projectileInstance = Object.Instantiate(Projectile, FirePoint.position, FirePoint.rotation);
                ProjectileInstances.Add(projectileInstance);

                float horizontalSpread = Random.Range(-SpreadAngle, SpreadAngle);
                float verticalSpread = Random.Range(-SpreadAngle, SpreadAngle);

                Vector3 spreadDirection = Quaternion.Euler(verticalSpread, horizontalSpread, 0) * FirePoint.forward;

                projectileInstance.transform.forward = spreadDirection;
            }
            OnShot.Invoke();
        }

        public override void Move()
        {
            List<GameObject> projectilesToRemove = new();
            foreach (GameObject projectile in ProjectileInstances)
            {
                projectile.transform.Translate(projectile.transform.forward * WeaponConfig.ProjectileSpeed * Time.deltaTime, Space.World);

                if (Vector3.Distance(projectile.transform.position, FirePoint.position) >= WeaponConfig.MaxRange)
                {
                    projectilesToRemove.Add(projectile);
                }
            }
            foreach (GameObject projectile in projectilesToRemove)
            {
                RemoveProjectile(projectile);
            }
        }
    }
}
