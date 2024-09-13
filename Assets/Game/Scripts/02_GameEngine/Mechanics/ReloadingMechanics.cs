using System;
using System.Collections;
using Atomic.Elements;
using Game.Scripts._02_GameEngine.AtomicObjects;
using Game.Scripts.Infrastructure.Services.Coroutines;
using GameEngine.AtomicObjects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class ReloadingMechanics
    {
        private IAtomicVariable<Weapon> _currentWeapon;
        private IAtomicObservable _shotEvent;
        private AmmoInventory _ammoInventory;
        private ICoroutineRunner _coroutineRunner;
        private bool _isReloading = false;
        private IAtomicObservable<PickupObject> _pickingUpCompleteEvent;

        public void Compose(
            IAtomicVariable<Weapon> currentWeapon,
            IAtomicObservable shotEvent,
            IAtomicObservable<PickupObject> pickingUpCompleteEvent,
            AmmoInventory ammoInventory,
            ICoroutineRunner coroutineRunner)
        {
            _pickingUpCompleteEvent = pickingUpCompleteEvent;
            _shotEvent = shotEvent;
            _ammoInventory = ammoInventory;
            _currentWeapon = currentWeapon;
            _coroutineRunner = coroutineRunner;
        }

        public void OnEnable()
        {
            _shotEvent.Subscribe(Check);
            _pickingUpCompleteEvent.Subscribe(Check);
        }

        public void OnDisable()
        {
            _shotEvent.Unsubscribe(Check);
            _pickingUpCompleteEvent.Unsubscribe(Check);
        }

        private void Check()
        {
            if (_currentWeapon?.Value?.HasAmmo.Value == false)
            {
                if (_ammoInventory.HasAmmo(_currentWeapon.Value.AmmoType))
                {
                    if (!_isReloading)
                    {
                        _coroutineRunner.StartCoroutine(Reload());
                    }
                }
            }
        }
        
        private void Check(PickupObject pickupObject)
        {
            if (pickupObject is Ammo ammo)
            {
                _ammoInventory.AddAmmo(ammo.AmmoType);
                ammo.Taken();
                Check();
            }
        }

        private IEnumerator Reload()
        {
            _isReloading = true;
            _currentWeapon.Value.ReloadProgressBar.Show();
            Debug.LogWarning("Reloading start ...");
            float factDuration = 0f;
            float progress = 0f;
            while (progress < 1f)
            {
                factDuration += Time.deltaTime;
                progress = factDuration / _currentWeapon.Value.Config.ReloadTime;
                _currentWeapon.Value.RefreshReloadProgress(progress);
                yield return null;
            }
            _currentWeapon.Value.Reload();
            Debug.LogWarning("Reloading complete!");
            _ammoInventory.UseAmmo(_currentWeapon.Value.AmmoType);
            _currentWeapon.Value.ReloadProgressBar.Hide();
            _isReloading = false;
        }
    }
}