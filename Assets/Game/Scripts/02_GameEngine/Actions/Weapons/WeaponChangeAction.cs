using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class WeaponChangeAction : IAtomicAction<Weapon>, IDisposable
    {
        private AtomicVariable<Weapon> _currentWeapon;

        [HideLabel]
        public WeaponTakeAction TakeAction;

        [HideLabel]
        public WeaponDropAction DropAction;

        public void Compose(Transform weaponOwner, Transform weaponParent, AtomicVariable<Weapon> currentWeapon)
        {
            _currentWeapon = currentWeapon;
            TakeAction.Compose(weaponParent);
            DropAction.Compose(weaponOwner, currentWeapon);
        }
        
        [Button("Change weapon", ButtonSizes.Small)]
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

        private void ChangeWeapon(Weapon newWeapon)
        {
            if (_currentWeapon.Value != null) 
                DropAction.Invoke();
            TakeAction.Invoke(newWeapon);
        }
        
        private void OnWeaponDrop(Weapon weapon) => 
            _currentWeapon.Value = null;

        private void OnWeaponTake(Weapon weapon) => 
            _currentWeapon.Value = weapon;

        public void Dispose()
        {
            _currentWeapon?.Dispose();
            DropAction?.Dispose();
            TakeAction?.Dispose();
        }
    }
}