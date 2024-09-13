using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine.Effects.Application
{
    [Serializable]
    public class SingleApplication : EffectApplicationStrategy
    {
        private IAtomicAction _singleAction;

        public void Compose(IAtomicAction singleAction)
        {
            _singleAction = singleAction;
        }
        
        public override void Apply()
        {
            Debug.LogError($"Разовый Action");
            _singleAction.Invoke();
        }
    }
}