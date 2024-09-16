using Atomic.Objects;
using GameEngine;
using GameEngine.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Traps
{
    public class DamageZone : Trap
    {
        [SerializeField]
        [BoxGroup("Damage Effect"), HideLabel]
        private DamageEffect _damageEffect;

        [SerializeField] private float _period = 1f;
        
        protected override void ApplyEffects(AtomicObject atomicObject)
        {
            if (atomicObject.Is(ObjectType.Damageable))
            {
                _damageEffect.ApplyEffect(atomicObject);
            }
        }
    }
}