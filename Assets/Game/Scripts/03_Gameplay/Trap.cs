using System.Collections.Generic;
using Atomic.Objects;
using GameEngine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class Trap : AtomicObject
    {
        [SerializeField]
        protected List<ActivateConditions> ActivateConditions;
        
        [SerializeField, Required]
        protected TriggerObserver TriggerObserver;
        
        [SerializeField]
        protected Animation Animation;
        
        protected void OnEnable()
        {
            TriggerObserver.TriggerEnter += OnTriggerEnter;
        }

        protected void OnDisable()
        {
            TriggerObserver.TriggerEnter -= OnTriggerEnter;
        }
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            foreach (ActivateConditions condition in ActivateConditions)
            {
                if (condition == Gameplay.ActivateConditions.Trigger)
                {
                    Activate(other);
                }
            }
        }

        protected virtual void Activate(Collider target)
        {
           
            AtomicObject atomicObject = target.GetComponentInParent<AtomicObject>();
            if (atomicObject) 
                ApplyEffects(atomicObject);
            
            CheckAnim();
        }

        private void CheckAnim()
        {
            if (Animation != null)
            {
                Animation.Play();
            }
        }

        protected virtual void OnTriggerExit(Collider other) { }
        
        protected virtual void ApplyEffects(AtomicObject atomicObject) { }
    }
    
    public enum ActivateConditions
    {
        Trigger = 0,
        Processing = 1,
        Projectile = 2,
        Explosion = 3,
    }
}