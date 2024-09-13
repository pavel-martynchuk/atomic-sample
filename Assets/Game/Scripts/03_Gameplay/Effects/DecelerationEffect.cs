using System;
using System.Collections;
using Atomic.Objects;
using GameEngine.Data;
using GameEngine.Effects.Application;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine.Effects
{
    [Serializable]
    public class DecelerationEffect : Effect
    {
        [SerializeField] [MinValue(0f), MaxValue(2f)] [SuffixLabel("%", Overlay = true)]
        private float _decelerationFactor = 1f;

        public override void ApplyEffect(AtomicObject atomicObject)
        {
            Stat speed = atomicObject.Get<Stat>(ObjectAPI.SpeedStat);
            
            switch (ApplicationStrategy)
            {
                case ContinuousPermanentApplication strategy:
                    StartCoroutine(DecelerateOnTime(speed, strategy.Duration));
                    break;
            }
        }

        private IEnumerator DecelerateOnTime(Stat speed, float duration)
        {
            speed.AddMultiplicativeModifier(_decelerationFactor);
            yield return new WaitForSeconds(duration);
            speed.RemoveMultiplicativeModifier(_decelerationFactor);
        }
    }
}