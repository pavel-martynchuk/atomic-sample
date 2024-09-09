using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons
{
    [CreateAssetMenu(menuName = "StaticData/WeaponConfig", fileName = "WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [Required]
        public GameObject AmmoPrefab;
        
        [Space(25f)]
        public int AmmoCapacity;
        public int MaxAmmoCarry;
        public float ReloadTime; 
        public float MaxRange; 
        public float MinDamage;
        public float MaxDamage;
        public float ProjectileSpeed;
    }
}