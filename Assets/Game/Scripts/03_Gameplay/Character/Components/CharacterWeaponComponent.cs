using Atomic.Elements;
using Game.Scripts.Gameplay.Weapons;
using Sirenix.OdinInspector;

namespace Game.Scripts.Gameplay.Character
{
    [Searchable]
    public sealed class CharacterWeaponComponent
    {
        public IAtomicVariable<Weapon> CurrentWeapon;
        
    }
}