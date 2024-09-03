using System;
using Atomic.Elements;
using Atomic.Objects;
using GameEngine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts._03_Gameplay
{
    public class Trap : MonoBehaviour
    {
        [SerializeField, Required]
        private TriggerObserver _triggerObserver;
        
        [SerializeField]
        private DealDamageAction _damageAction = new();  
        
        [SerializeField]
        private int _damage = 3;
        
        private void Awake()
        {
            _damageAction.Compose(new AtomicValue<int>(_damage));
        }

        private void OnEnable()
        {
            _triggerObserver.TriggerEnter += DoDamage;
        }

        private void OnDisable()
        {
            _triggerObserver.TriggerEnter -= DoDamage;
        }

        private void DoDamage(Collider obj)
        {
            AtomicObject atomicObject = obj.GetComponentInParent<AtomicObject>();
            
            if (atomicObject != null)  
                _damageAction.Invoke(atomicObject);
        }
    }
}