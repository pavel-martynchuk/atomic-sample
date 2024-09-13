using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine.Effects.Application
{
    [Serializable]
    public class ContinuousPermanentApplication : EffectApplicationStrategy
    {
        public float Duration;

        private AtomicAction _applyAction;
        private AtomicAction _removeAction;

        public void Compose(AtomicAction applyAction, AtomicAction removeAction)
        {
            _applyAction = applyAction;
            _removeAction = removeAction;
        }

        public override void Apply()
        {
            Debug.LogError($"Продолжительное наложение");
            _applyAction.Invoke();
        }
        
        public void Remove(GameObject target)
        {
            Debug.LogError($"Снятие продолжительного наложения");
            _removeAction.Invoke();
        }
    }
}