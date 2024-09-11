using System;
using GameEngine;
using Atomic.Elements;
using GameEngine.AtomicObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Character
{
    [Serializable]
    public sealed class CharacterWeaponComponent : IDisposable
    {
        public AtomicVariable<Weapon> CurrentWeapon => _currentWeapon;

        [SerializeField, ReadOnly, InlineProperty]
        private AtomicVariable<Weapon> _currentWeapon = new();

        public WeaponChangeAction WeaponChangeAction;
        private IAtomicObservable<PickupObject> _pickupEvent;

        public void Compose(
            Transform weaponOwner,
            Transform weaponParent,
            IAtomicObservable<PickupObject> pickupEvent)
        {
            _pickupEvent = pickupEvent;
            WeaponChangeAction.Compose(weaponOwner, weaponParent, _currentWeapon);
        }
        
        public void OnEnable()
        {
            _pickupEvent.Subscribe(WeaponChangeAction.TryToTakePickup);
            WeaponChangeAction.OnEnable();
        }
        
        public void OnDisable()
        {
            _pickupEvent.Unsubscribe(WeaponChangeAction.TryToTakePickup);
            WeaponChangeAction.OnDisable();
        }

        public void Dispose()
        {
            _currentWeapon?.Dispose();
            WeaponChangeAction?.Dispose();
        }
    }
}