using System;
using Atomic.Objects;
using GameEngine.Effects.Application;
using UnityEngine;

namespace GameEngine.Effects
{
    [Serializable]
    public class ThrowbackEffect : Effect
    {
        [SerializeField] private ThrowDirection _throwDirection;
        [SerializeField] private float _force;
        
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
            switch (_throwDirection)
            {
                case ThrowDirection.Left:
                    return transform.right;
                case ThrowDirection.Right:
                    return -transform.right;
                case ThrowDirection.Back:
                    return -transform.forward;
                case ThrowDirection.ToTarget:
                    return ((target.position + Vector3.up) - transform.position).normalized;
                case ThrowDirection.Forward:
                default: return transform.forward;
            }
        }
    }

    public enum ThrowDirection
    {
        Left = 0,
        Right = 1,
        Forward = 2,
        Back = 3,
        ToTarget = 4,
    }
}