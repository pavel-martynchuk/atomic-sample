using System;
using Atomic.Elements;
using GameEngine.AtomicObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class WeaponChangeAction : IAtomicAction<Weapon>, IDisposable
    {
        private AtomicVariable<Weapon> _currentWeapon;

        [HideLabel]
        public WeaponDropAction DropAction;

        [HideLabel]
        public WeaponTakeAction TakeAction;

        private TargetCaptureMechanics _targetCaptureMechanics;

        public void Compose(Transform weaponOwner,
            Transform weaponParent,
            AtomicVariable<Weapon> currentWeapon,
            TargetCaptureMechanics targetCaptureMechanics)
        {
            _currentWeapon = currentWeapon;
            TakeAction.Compose(weaponParent);
            DropAction.Compose(weaponOwner, currentWeapon);
            _targetCaptureMechanics = targetCaptureMechanics;
        }
        
        [Button("Change weapon")]
        public void Invoke(Weapon newWeapon)
        {
            ChangeWeapon(newWeapon);
        }

        public void OnEnable()
        {
            DropAction.WeaponDropEvent.Subscribe(OnWeaponDrop);
            TakeAction.WeaponTakeEvent.Subscribe(OnWeaponTake);
        }
        
        public void OnDisable()
        {
            DropAction.WeaponDropEvent.Unsubscribe(OnWeaponDrop);
            TakeAction.WeaponTakeEvent.Unsubscribe(OnWeaponTake);
        }
        
        public void TryToTakePickup(PickupObject pickupObject)
        {
            if (pickupObject is Weapon newWeapon)
            {
                ChangeWeapon(newWeapon);
            }
        }

        private void ChangeWeapon(Weapon newWeapon)
        {
            if (_currentWeapon.Value != null)
            {
                DropAction.Invoke();
            }
            TakeAction.Invoke(newWeapon);
        }
        
        private void OnWeaponDrop(Weapon weapon)
        {
            _currentWeapon.Value.OnDropped();
            _currentWeapon.Value = null;
        }

        private void OnWeaponTake(Weapon weapon)
        {
            _currentWeapon.Value = weapon;
            _currentWeapon.Value.OnTaken(_targetCaptureMechanics);
        }

        public void Dispose()
        {
            _currentWeapon?.Dispose();
            DropAction?.Dispose();
            TakeAction?.Dispose();
        }
    }
}