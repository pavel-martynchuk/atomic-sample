using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.GameEngine.UI;
using GameEngine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    [Is(ObjectType.PickUp)]
    public abstract class PickupObject : AtomicObject
    {
        [SerializeField]
        [MinValue(0f)]
        protected float PickupDuration = 1f;

        [SerializeField, Required]
        protected AtomicAction PickUpAction;
        
        [SerializeField, Required]
        protected Collider TriggerCollider;
        
        [SerializeField, Required]
        protected RingSliderFillBar PickupProgress;
        
        protected virtual void Compose(float pickupDuration)
        {
            PickupDuration = pickupDuration;
        }

        public void Select()
        {
            PickupProgress.Show();
        } 
        
        public void Deselect()
        {
            PickupProgress.Hide();
        }
    }
}