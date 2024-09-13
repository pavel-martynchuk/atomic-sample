using UnityEngine;

namespace GameEngine
{
    public static class ObjectAPI
    {
        [Header("Character")]
        public const string MoveDirection = nameof(MoveDirection);
        public const string SpeedStat = nameof(SpeedStat);
        public const string RotateDirection = nameof(RotateDirection);
        public const string DashAction = nameof(DashAction);
        public const string FireAction = nameof(FireAction);
        public const string Rigidbody = nameof(Rigidbody);
        public const string RagdollComponent = nameof(RagdollComponent);
        public const string AccelerateMechanics = nameof(AccelerateMechanics);
        public const string PickupMechanics = nameof(PickupMechanics);
        public const string TakeDamageAction = nameof(TakeDamageAction);
    }
}