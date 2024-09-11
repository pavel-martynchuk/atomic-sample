using Atomic.Elements;
using GameEngine.Data;
using UnityEngine;

namespace GameEngine
{
    public class BallisticShot : ShotStrategy
    {
        private Rigidbody _rigidbody;
        private Projectile _projectileInstance;
        private readonly IAtomicVariable<Transform> _target;

        public BallisticShot(Projectile projectile, Transform firePoint, WeaponConfig weaponConfig,
            IAtomicVariable<Transform> target) : base(projectile, firePoint, weaponConfig)
        {
            _target = target;
        }

        public override void Shot()
        {
            _projectileInstance = Object.Instantiate(Projectile, FirePoint.position, FirePoint.rotation);

            _rigidbody = _projectileInstance.GetComponent<Rigidbody>();

            if (_rigidbody == null)
            {
                Debug.LogError("Projectile does not have a Rigidbody!");
                return;
            }
            _rigidbody.useGravity = true;
            _rigidbody.velocity = Vector3.zero;
            Vector3 velocity = CalculateBallisticVelocity(FirePoint.position, _target.Value.position, WeaponConfig.ProjectileSpeed);
            _rigidbody.AddForce(velocity, ForceMode.VelocityChange);
        }

        public override void Move() { }

        private Vector3 CalculateBallisticVelocity(Vector3 origin, Vector3 target, float initialSpeed)
        {
            Vector3 toTarget = target - origin;
            Vector3 toTargetXZ = new(toTarget.x, 0, toTarget.z);

            float yOffset = toTarget.y;
            float xzDistance = toTargetXZ.magnitude;

            float time = xzDistance / initialSpeed;

            float verticalVelocity = (yOffset / time) + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

            Vector3 result = toTargetXZ.normalized * initialSpeed;
            result.y = verticalVelocity;

            return result;
        }
    }
}
