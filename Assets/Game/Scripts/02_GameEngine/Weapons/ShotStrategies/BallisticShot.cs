using GameEngine.Data;
using UnityEngine;

namespace GameEngine
{
    public class BallisticShot : ShotStrategy
    {
        public TargetCaptureMechanics TargetCaptureMechanics
        {
            set => _targetCaptureMechanics = value;
        }

        private TargetCaptureMechanics _targetCaptureMechanics;
        private const float LaunchAngle = 60f;

        public BallisticShot(GameObject projectile, Transform firePoint, WeaponConfig weaponConfig)
            : base(projectile, firePoint, weaponConfig)
        {
        }

        public override void Shot()
        {
            Vector3 targetPosition;

            if (_targetCaptureMechanics?.CurrentTarget?.Value == null)
            {
                targetPosition = FirePoint.position + FirePoint.forward * WeaponConfig.MaxRange;
            }
            else
            {
                targetPosition = _targetCaptureMechanics.CurrentTarget.Value.position;

                if (Vector3.Distance(FirePoint.position, targetPosition) < 0.1f)
                {
                    Debug.LogWarning("The target is too close.");
                    return;
                }
            }

            GameObject projectileInstance = Object.Instantiate(Projectile, FirePoint.position, Quaternion.identity);
            ProjectileInstances.Add(projectileInstance);

            Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("The projectile does not have a Rigidbody component.");
                return;
            }

            Vector3 velocity = CalculateLaunchVelocity(targetPosition);
            if (float.IsNaN(velocity.x) || float.IsNaN(velocity.y) || float.IsNaN(velocity.z))
            {
                Debug.LogError("The calculated speed contains incorrect values.");
                return;
            }

            rb.velocity = velocity;
            OnShot.Invoke();
        }

        public override void Move() { }

        private Vector3 CalculateLaunchVelocity(Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - FirePoint.position;
            float horizontalDistance = new Vector3(direction.x, 0, direction.z).magnitude;

            if (horizontalDistance == 0)
            {
                Debug.LogError("Горизонтальная дистанция равна нулю.");
                return Vector3.zero;
            }

            float heightDifference = direction.y;
            float launchAngleRadians = LaunchAngle * Mathf.Deg2Rad;
            float gravity = Mathf.Abs(Physics.gravity.y);
            float cosLaunchAngle = Mathf.Cos(launchAngleRadians);

            float velocitySquared = (gravity * horizontalDistance * horizontalDistance) /
                (2 * cosLaunchAngle * cosLaunchAngle * (horizontalDistance * Mathf.Tan(launchAngleRadians) - heightDifference));

            if (velocitySquared <= 0)
            {
                Debug.LogError("Рассчитанное квадрат скорости имеет некорректное значение.");
                return Vector3.zero;
            }

            float initialVelocity = Mathf.Sqrt(velocitySquared);
            Vector3 velocity = new Vector3(direction.x, 0, direction.z).normalized * initialVelocity * cosLaunchAngle;
            velocity.y = initialVelocity * Mathf.Sin(launchAngleRadians);

            return velocity;
        }
    }
}
