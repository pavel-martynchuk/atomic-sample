using Atomic.Objects;
using GameEngine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Weapons
{
    [Is(ObjectType.Weapon)]
    public class Weapon : PickupObject
    {
        [SerializeField, Required]
        private WeaponConfig _config;
        
        protected override void Compose(float pickupDuration)
        {
            base.Compose(pickupDuration);
            
        }
    }
}