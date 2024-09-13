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
        [BoxGroup("Damage Effect"), HideLabel]
        private DamageEffect _damageEffect;

        public Vector3 GetPosition() => 
            transform.position;
        
        protected override void ApplyEffects(AtomicObject atomicObject)
        {
            if (atomicObject.Is(ObjectType.Damageable))
            {
                _damageEffect.ApplyEffect(atomicObject);
            }
        }
    }
}