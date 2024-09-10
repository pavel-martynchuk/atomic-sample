using System;
using GameEngine;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Character
{
    [Serializable]
    public sealed class CharacterWeaponComponent : IDisposable
    {
        [SerializeField, ReadOnly, InlineProperty]
        private AtomicVariable<Weapon> _currentWeapon = new();

        public WeaponChangeAction WeaponChangeAction;

        public void Compose(Transform weaponOwner, Transform weaponParent)
        {
            WeaponChangeAction.Compose(weaponOwner, weaponParent, _currentWeapon);
        }
        
        public void OnEnable()
        {
            WeaponChangeAction.OnEnable();
        }
        
        public void OnDisable()
        {
            WeaponChangeAction.OnDisable();
        }

        public void Dispose()
        {
            _currentWeapon?.Dispose();
            WeaponChangeAction?.Dispose();
        }
    }
}