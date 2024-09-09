using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons
{
    public class Weapon : PickupObject
    {
        [SerializeField, Required]
        private WeaponConfig _config;
        
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