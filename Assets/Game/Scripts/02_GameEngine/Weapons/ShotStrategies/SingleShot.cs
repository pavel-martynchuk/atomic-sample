using System.Collections.Generic;
using GameEngine.Data;
using UnityEngine;

namespace GameEngine
{
    public class SingleShot : ShotStrategy
    {
        public SingleShot(GameObject projectile, Transform firePoint,
            WeaponConfig weaponConfig) : base(projectile, firePoint, weaponConfig)
        {
        }

        public override void Shot()
        {
            GameObject projectileInstance = Object.Instantiate(Projectile, FirePoint.position, FirePoint.rotation);
            projectileInstance.transform.forward = FirePoint.forward;
            ProjectileInstances.Add(projectileInstance);
            OnShot.Invoke();
        }

        public override void Move()
        {
            List<GameObject> projectilesToRemove = new();

            foreach (GameObject projectile in ProjectileInstances)
            {
                Transform projectileTransform = projectile.transform;
                projectileTransform.Translate(projectileTransform.forward * WeaponConfig.ProjectileSpeed * Time.deltaTime, Space.World);
                if (Vector3.Distance(projectileTransform.position, FirePoint.position) >= WeaponConfig.MaxRange)
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