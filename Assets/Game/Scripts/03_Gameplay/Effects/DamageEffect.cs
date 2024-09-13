using System;
using Atomic.Elements;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine.Effects
{
    [Serializable]
    public class DamageEffect : Effect
    {
        [SerializeField, InlineProperty]
        private AtomicValue<int> _damageValue;

        [SerializeField]
        private DealDamageAction _damageAction = new();

        public override void ApplyEffect(AtomicObject atomicObject)
        {
            _damageAction.Compose(_damageValue);
            _damageAction.Invoke(atomicObject);
        }
    }
}