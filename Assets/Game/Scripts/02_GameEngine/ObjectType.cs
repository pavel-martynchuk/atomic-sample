namespace GameEngine
{
    public static class ObjectType
    {
        public const string Damageable = nameof(ObjectTypes.Damageable);
        public const string Moveable = nameof(ObjectTypes.Moveable);
        public const string Physical = nameof(ObjectTypes.Physical);
    }

    public enum ObjectTypes
    {
        Damageable = 0,
        Moveable = 1,
        Physical = 1,
    }
}