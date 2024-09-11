using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class WeaponTakeAction : IAtomicAction<Weapon>, IDisposable
    {
        public IAtomicObservable<Weapon> WeaponTakeEvent => _weaponTakeEvent;

        private Transform _weaponParent;
        private AtomicEvent<Weapon> _weaponTakeEvent;
        
        public void Compose(Transform weaponParent)
        {
            _weaponParent = weaponParent;
            _weaponTakeEvent = new AtomicEvent<Weapon>();
        }
        
        [Button("Take weapon")]
        public void Invoke(Weapon weapon)
        {
            TakeWeapon(weapon);
        }

        public void TakeWeapon(Weapon weapon)
        {
            weapon.transform.SetParent(_weaponParent, worldPositionStays: true);
            weapon.transform.localRotation = Quaternion.identity;
            PositionWeaponByPivot(weapon);
            _weaponTakeEvent.Invoke(weapon);
        }
        
        public void PositionWeaponByPivot(Weapon weapon)
        {
            Vector3 pivotWorldPosition = weapon.Pivot.position;
            Vector3 parentWorldPosition = _weaponParent.position;
            Vector3 offset = parentWorldPosition - pivotWorldPosition;
            weapon.transform.position += offset;
        }

        public void Dispose()
        {
            _weaponTakeEvent?.Dispose();
        }
    }
}