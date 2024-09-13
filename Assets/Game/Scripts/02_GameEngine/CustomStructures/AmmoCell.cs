using System;

namespace GameEngine
{
    [Serializable]
    public class AmmoCell
    {
        public AmmoType AmmoType;
        public int Count;
    }
    
    public enum AmmoType
    {
        Unknown = 0,
        Arrow = 1,
        Bullet = 2,
        Shell = 3,
        Grenade = 4,
    }
}