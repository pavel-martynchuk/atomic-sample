using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public class AmmoInventory
    {
        [SerializeField]
        private int _maxAmmoPerType = 3;

        [ShowInInspector]
        private Dictionary<AmmoType, AmmoCell> _ammoInventory = new();

        public bool HasAmmo(AmmoType ammoType)
        {
            if (ammoType == AmmoType.Unknown)
            {
                Debug.LogError("Тип боеприпасов не определён.");
                return false;
            }

            return _ammoInventory.ContainsKey(ammoType) && _ammoInventory[ammoType].Count > 0;
        }

        public bool UseAmmo(AmmoType ammoType)
        {
            if (ammoType == AmmoType.Unknown)
            {
                Debug.LogError("Невозможно использовать несуществующий тип боеприпасов.");
                return false;
            }

            if (!HasAmmo(ammoType))
            {
                Debug.LogWarning($"Нет боеприпасов типа {ammoType}.");
                return false;
            }

            _ammoInventory[ammoType].Count -= 1;

            if (_ammoInventory[ammoType].Count == 0)
            {
                _ammoInventory.Remove(ammoType);
            }

            Debug.Log($"Использован 1 боеприпас типа {ammoType}. Осталось: {(_ammoInventory.TryGetValue(ammoType, out AmmoCell cell) ? cell.Count : 0)}.");
            return true;
        }

        public bool AddAmmo(AmmoType ammoType)
        {
            if (ammoType == AmmoType.Unknown)
            {
                Debug.LogError("Невозможно добавить боеприпасы с неопределённым типом.");
                return false;
            }

            if (!_ammoInventory.ContainsKey(ammoType))
            {
                _ammoInventory[ammoType] = new AmmoCell { AmmoType = ammoType, Count = 0 };
            }

            AmmoCell ammoCell = _ammoInventory[ammoType];

            if (ammoCell.Count >= _maxAmmoPerType)
            {
                Debug.LogWarning($"Инвентарь боеприпасов типа {ammoType} заполнен. Максимум: {_maxAmmoPerType}.");
                return false;
            }

            ammoCell.Count += 1;
            Debug.Log($"Добавлен 1 боеприпас типа {ammoType}. Теперь у вас {ammoCell.Count}.");

            return true;
        }

        public int GetAmmoCount(AmmoType ammoType)
        {
            if (ammoType == AmmoType.Unknown)
            {
                Debug.LogError("Невозможно получить количество для неопределённого типа боеприпасов.");
                return 0;
            }

            return _ammoInventory.TryGetValue(ammoType, out AmmoCell ammoCell) ? ammoCell.Count : 0;
        }
    }
}
