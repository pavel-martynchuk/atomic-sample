using System;
using UnityEngine;

namespace GameEngine.Effects.Application
{
    [Serializable]
    public class ContinuousSingleApplication : EffectApplicationStrategy
    {
        public float Duration;
        public float Interval;
        
        public override void Apply()
        {
            Debug.LogError($"Продолжительное наложение с одиночными действиями");
        }
    }
}