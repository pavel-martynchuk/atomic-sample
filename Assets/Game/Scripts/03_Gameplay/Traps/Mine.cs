using Atomic.Objects;
using GameEngine;
using GameEngine.Effects;
using GameEngine.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Traps
{
    public class Mine : Trap, ITargeted
    {
        [SerializeField]
        [BoxGroup("DamageEffect"), HideLabel]
        private DamageEffect _damageEffect;

        [SerializeField]
        [BoxGroup("ThrowbackEffect"), HideLabel]
        private ThrowbackEffect _throwbackEffect;
        
        public Vector3 GetPosition() => 
            transform.position;
        
        protected override void ApplyEffects(AtomicObject atomicObject)
        {
            if (atomicObject.Is(ObjectType.Damageable))
            {
                Debug.LogError(1);
                _damageEffect.ApplyEffect(atomicObject);
            }
            if (atomicObject.Is(ObjectType.Physical))
            {
                _throwbackEffect.ApplyEffect(atomicObject);
            }

        }
    }
}