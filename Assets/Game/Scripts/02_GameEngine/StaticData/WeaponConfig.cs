using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine.Data
{
    [CreateAssetMenu(menuName = "StaticData/WeaponConfig", fileName = "WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [Required]
        public GameObject ProjectilePrefab;
        
        [Space(25f)]
        public int AmmoCapacity;
        public int MaxAmmoCarry;
        public float ReloadTime; 
        public float MaxRange; 
        public int MinDamage;
        public int MaxDamage;
        public float ProjectileSpeed;
    }
}