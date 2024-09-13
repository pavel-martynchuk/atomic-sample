using Atomic.Objects;
using Game.Scripts.Infrastructure.Services.Coroutines;
using GameEngine;
using GameEngine.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Traps
{
    public class Snare : Trap, ICoroutineRunner
    {
        [SerializeField]
        [BoxGroup("Damage Effect"), HideLabel]
        private DamageEffect _damageEffect;
        
        [SerializeField]
        [BoxGroup("Deceleration Effect"), HideLabel]
        private DecelerationEffect _decelerationEffect;

        protected override void ApplyEffects(AtomicObject atomicObject)
        {
            if (atomicObject.Is(ObjectType.Damageable))
            {
                _damageEffect.ApplyEffect(atomicObject);
            }
            if (atomicObject.Is(ObjectType.Moveable))
            {
                _decelerationEffect.ApplyEffect(atomicObject);
            }
        }
    }
}