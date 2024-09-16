using Atomic.Objects;
using GameEngine;
using GameEngine.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Traps
{
    public class SlowZone : Trap
    {
        [SerializeField]
        [BoxGroup("Deceleration Effect"), HideLabel]
        private DecelerationEffect _decelerationEffect;

        protected override void ApplyEffects(AtomicObject atomicObject)
        {
            if (atomicObject.Is(ObjectType.Moveable))
            {
                _decelerationEffect.ApplyEffect(atomicObject);
            }
        }

        protected override void RemoveEffects(AtomicObject atomicObject)
        {
            if (atomicObject.Is(ObjectType.Moveable))
            {
                _decelerationEffect.RemoveEffect(atomicObject);
            }
        }
    }
}