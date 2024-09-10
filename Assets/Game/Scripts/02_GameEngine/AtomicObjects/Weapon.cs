using GameEngine.AtomicObjects;
using GameEngine.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    public class Weapon : PickupObject
    {
        public Transform Pivot => _pivot;

        [SerializeField, Required]
        private WeaponConfig _config;

        [SerializeField, Required]
        private Transform _pivot;
        
        
        protected override void Compose(float pickupDuration)
        {
            base.Compose(pickupDuration);
        }

        public override void Use()
        {
            base.Use();
            Debug.LogError("Use - Weapon");
        }
    }
}