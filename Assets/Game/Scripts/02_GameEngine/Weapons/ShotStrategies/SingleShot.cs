using GameEngine.Data;
using UnityEngine;

namespace GameEngine
{
    public class SingleShot : ShotStrategy
    {
        private Projectile _projectileInstance;
        public SingleShot(Projectile projectile, Transform firePoint,
            WeaponConfig weaponConfig) : base(projectile, firePoint, weaponConfig)
        {
        }

        public override void Shot()
        {
            _projectileInstance = Object.Instantiate(Projectile, FirePoint.position, FirePoint.rotation);
            _projectileInstance.transform.forward = FirePoint.forward;
        }

        public override void Move()
        {
            if (_projectileInstance == null)
                return;

            _projectileInstance.transform.Translate(_projectileInstance.transform.forward * WeaponConfig.ProjectileSpeed * Time.deltaTime);

            if (Vector3.Distance(_projectileInstance.transform.position, FirePoint.position) >= WeaponConfig.MaxRange)
            {
                Object.Destroy(_projectileInstance.gameObject);
            }
        }
    }
}