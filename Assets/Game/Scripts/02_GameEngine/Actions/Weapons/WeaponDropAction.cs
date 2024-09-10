using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class WeaponDropAction : IAtomicAction, IDisposable
    { 
        public IAtomicObservable<Weapon> WeaponDropEvent => _weaponDropEvent;

        private IAtomicVariable<Weapon> _currentWeapon;
        private AtomicEvent<Weapon> _weaponDropEvent;        
        private Transform _weaponOwner;        
        
        public void Compose(Transform owner, IAtomicVariable<Weapon> currentWeapon)
        {
            _weaponOwner = owner;
            _currentWeapon = currentWeapon;
            _weaponDropEvent = new AtomicEvent<Weapon>();
        }
        
        [Button("Drop weapon", ButtonSizes.Small)]
        public void Invoke()
        {
            DropWeapon(_currentWeapon.Value);
        }
        
        public void DropWeapon(Weapon weapon)
        {
            weapon.transform.SetParent(null);
            PositionWeaponOnGround(weapon);
            _weaponDropEvent.Invoke(weapon);
        }
        
        public void PositionWeaponOnGround(Weapon weapon)
        {
            Vector3 ownerPosition = _weaponOwner.position;
            Vector3 droppedPlacePosition = ownerPosition + (_weaponOwner.forward * 2f);
            droppedPlacePosition.y = 0f;
            weapon.transform.position = droppedPlacePosition;
        }

        public void Dispose()
        {
            _weaponDropEvent?.Dispose();
        }
    }
}