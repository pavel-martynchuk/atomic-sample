using System;
using System.Collections.Generic;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine.Data
{
    [Serializable]
    public class Stat : AtomicVariable<float>
    {
        [LabelText("Base Value"), PropertyOrder(0)]
        [ShowInInspector, ReadOnly]
        private float BaseValue => base.Value;
        
        [LabelText("Result Value"), PropertyOrder(0)]
        [ShowInInspector, ReadOnly]
        public override float Value
        {
            get
            {
                float finalValue = base.Value;

                foreach (float mod in additiveModifiers)
                {
                    finalValue += mod;
                }

                foreach (float mod in multiplicativeModifiers)
                {
                    finalValue *= mod;
                }

                return finalValue;
            }
            set
            {
                base.Value = value;
                OnChanged?.Invoke(Value);
            }
        }
        
        [Space(24)]
        [SerializeField, ReadOnly, PropertyOrder(1)]
        private List<float> additiveModifiers = new();

        [SerializeField, ReadOnly, PropertyOrder(1)]
        private List<float> multiplicativeModifiers = new();

        public Stat(float value)
        {
            base.Value = value;
        }

        public void AddAdditiveModifier(float value)
        {
            if (value != 0f)
            {
                additiveModifiers.Add(value);
                OnChanged?.Invoke(Value);
            }
        }

        public void RemoveAdditiveModifier(float value)
        {
            if (additiveModifiers.Contains(value))
            {
                additiveModifiers.Remove(value);
                OnChanged?.Invoke(Value);
            }
        }

        public void AddMultiplicativeModifier(float value)
        {
            if (!Mathf.Approximately(value, 1f))
            {
                multiplicativeModifiers.Add(value);
                OnChanged?.Invoke(Value);
            }
        }

        public void RemoveMultiplicativeModifier(float value)
        {
            if (multiplicativeModifiers.Contains(value))
            {
                multiplicativeModifiers.Remove(value);
                OnChanged?.Invoke(Value);
            }
        }

        public void ResetModifiers()
        {
            additiveModifiers.Clear();
            multiplicativeModifiers.Clear();
            OnChanged?.Invoke(Value);
        }
    }
}