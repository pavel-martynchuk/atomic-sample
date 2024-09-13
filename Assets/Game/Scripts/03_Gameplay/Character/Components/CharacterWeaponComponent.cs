using System;
using GameEngine;
using Atomic.Elements;
using Game.Scripts.Infrastructure.Services.Coroutines;
using GameEngine.AtomicObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Character
{
    [Serializable]
    public sealed class CharacterWeaponComponent : IDisposable
    {
        public IAtomicVariable<Weapon> CurrentWeapon => _currentWeapon;

        [SerializeField, ReadOnly, InlineProperty]
        private AtomicVariable<Weapon> _currentWeapon;

        [SerializeField] 
        private WeaponChangeAction _weaponChangeAction;
        
        [SerializeField, ReadOnly] 
        private ReloadingMechanics _reloadingMechanics;
        
        private CharacterInventoryComponent _characterInventoryComponent;
        private IAtomicObservable<PickupObject> _pickupEvent;

        public void Compose(Transform weaponOwner,
            Transform weaponParent,
            IAtomicObservable<PickupObject> pickupEvent,
            TargetCaptureMechanics targetCaptureMechanics,
            CharacterInventoryComponent characterInventoryComponent,
            IAtomicObservable fireAction,
            ICoroutineRunner coroutineRunner)
        {
            _characterInventoryComponent = characterInventoryComponent;
            _pickupEvent = pickupEvent;
            
            _weaponChangeAction.Compose(weaponOwner, weaponParent, _currentWeapon, targetCaptureMechanics);
            
            _reloadingMechanics.Compose(
                _currentWeapon, fireAction, pickupEvent, _characterInventoryComponent.AmmoInventory, coroutineRunner);
        }
        
        public void OnEnable()
        {
            _pickupEvent.Subscribe(_weaponChangeAction.TryToTakePickup);
            _weaponChangeAction.OnEnable();
            _reloadingMechanics.OnEnable();
        }
        
        public void OnDisable()
        {
            _pickupEvent.Unsubscribe(_weaponChangeAction.TryToTakePickup);
            _weaponChangeAction.OnDisable();
            _reloadingMechanics.OnDisable();
        }

        public void Dispose()
        {
            _currentWeapon?.Dispose();
            _weaponChangeAction?.Dispose();
        }
    }
}