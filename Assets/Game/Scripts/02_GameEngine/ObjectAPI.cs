using UnityEngine;

namespace GameEngine
{
    public static class ObjectAPI
    {
        [Header("Character")]
        public const string MoveDirection = nameof(MoveDirection);
        public const string RotateDirection = nameof(RotateDirection);
        public const string DashAction = nameof(DashAction);
        public const string FireAction = nameof(FireAction);
        public const string AccelerateMechanics = nameof(AccelerateMechanics);
        public const string PickupMechanics = nameof(PickupMechanics);
        public const string TakeDamageAction = nameof(TakeDamageAction);
    }
}