using System;
using GameEngine;
using Sirenix.OdinInspector;

namespace Game.Scripts.Gameplay.Character
{
    [Serializable]
    public class CharacterInventoryComponent
    {
        public AmmoInventory AmmoInventory => _ammoInventory;

        [ShowInInspector]
        private AmmoInventory _ammoInventory = new AmmoInventory();

        public bool HasAmmo(AmmoType ammoType)
        {
            return _ammoInventory.HasAmmo(ammoType);
        }

        [Button]
        public bool UseAmmo(AmmoType ammoType)
        {
            return _ammoInventory.UseAmmo(ammoType);
        }

        [Button]
        public bool AddAmmo(AmmoType ammoType)
        {
            return _ammoInventory.AddAmmo(ammoType);
        }

        public int GetAmmoCount(AmmoType ammoType)
        {
            return _ammoInventory.GetAmmoCount(ammoType);
        }
    }
}