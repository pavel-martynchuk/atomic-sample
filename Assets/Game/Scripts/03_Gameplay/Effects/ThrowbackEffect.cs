using System;
using Atomic.Objects;
using GameEngine.Effects.Application;
using UnityEngine;

namespace GameEngine.Effects
{
    [Serializable]
    public class ThrowbackEffect : Effect
    {
        public ThrowDirection ThrowDirection => _throwDirection;

        [SerializeField] private ThrowDirection _throwDirection;
        [SerializeField] private float _force;
        [SerializeField, Range(0, 90)] private float _forceAngle = 30f;

        
        public override void ApplyEffect(AtomicObject atomicObject)
        {
            RagdollComponent ragdollComponent = atomicObject.Get<RagdollComponent>(ObjectAPI.RagdollComponent);
            Rigidbody rb = atomicObject.Get<Rigidbody>(ObjectAPI.Rigidbody);
            ragdollComponent?.EnableRagdoll();
            switch (ApplicationStrategy)
            {
                case SingleApplication:
                    rb.AddForce(GetDirection(rb.transform) * _force, ForceMode.Impulse);
                    break;
            }
        }

        public Vector3 GetDirection(Transform target)
        {
            Vector3 direction;
            switch (_throwDirection)
            {
                case ThrowDirection.Left:
                    direction = -transform.right;
                    break;
                case ThrowDirection.Right:
                    direction = transform.right;
                    break;
                case ThrowDirection.Back:
                    direction = -transform.forward;
                    break;
                case ThrowDirection.ToTarget:
                    direction = (target.position - transform.position).normalized;
                    break;
                case ThrowDirection.Up:
                    direction = Vector3.up;
                    break;
                case ThrowDirection.Forward:
                default:
                    direction = transform.forward;
                    break;
            }

            return Quaternion.AngleAxis(_forceAngle, transform.right) * direction;
        }
    }

    public enum ThrowDirection
    {
        Left = 0,
        Right = 1,
        Forward = 2,
        Back = 3,
        ToTarget = 4,
        Up = 5,
    }
}