using System;
using Atomic.Objects;
using GameEngine.Effects.Application;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine.Effects
{
    [Serializable]
    public abstract class Effect : MonoBehaviour
    {
        public LayerMask Targets;
        
        [SerializeReference, BoxGroup, HideLabel] 
        public EffectApplicationStrategy ApplicationStrategy;

        public abstract void ApplyEffect(AtomicObject atomicObject);
        public virtual void RemoveEffect(AtomicObject atomicObject) {}
    }
}