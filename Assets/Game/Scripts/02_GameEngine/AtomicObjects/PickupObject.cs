using Atomic.Objects;
using Game.Scripts.GameEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine.AtomicObjects
{
    public abstract class PickupObject : AtomicObject
    {
        [MinValue(0f)]
        public float PickupDuration = 1f;
        
        [SerializeField, Required]
        protected Collider TriggerCollider;
        
        [SerializeField, Required]
        protected RingSliderFillBar PickupProgress;
        
        protected virtual void Compose(float pickupDuration)
        {
            PickupDuration = pickupDuration;
        }

        protected virtual void Start()
        {
            Deselect();
        }

        public void Select()
        {
            PickupProgress.Show();
        } 
        
        public void Deselect()
        {
            PickupProgress.Hide();
        }
        
        public void RefreshPickupProgress(float value)
        {
            if (value is < 0f or > 1f)
            {
                Debug.LogWarning("Invalid value for - RefreshPickupProgress");
                value = Mathf.Clamp01(value);
            }
            PickupProgress.Refresh(value);
        }

        public virtual void OnTaken()
        {
            TriggerCollider.enabled = false;
            PickupProgress.Hide();
        }
        
        public virtual void OnDropped()
        {
            TriggerCollider.enabled = true;
        }
    }
}