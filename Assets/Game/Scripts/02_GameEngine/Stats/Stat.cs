using System;
using System.Collections.Generic;
using Atomic.Elements;
using Sirenix.OdinInspector;

namespace GameEngine.Data
{
    [Serializable]
    public class Stat : AtomicVariable<float>
    {
        public override float Value
        {
            get => FinalSpeed;
            set
            {
                base.Value = value;
                InvokeOnChanged();
            }
        }
        
        [ShowInInspector] private readonly List<float> _additiveModifiers = new();
        [ShowInInspector] private readonly List<float> _multiplicativeModifiers = new();
        
        public Stat(float movementSpeed)
        {
            Value = movementSpeed;
        }

        public float FinalSpeed
        {
            get
            {
                float baseSpeed = Value;

                foreach (var mod in _additiveModifiers)
                {
                    baseSpeed += mod;
                }

                foreach (var multiplier in _multiplicativeModifiers)
                {
                    baseSpeed *= multiplier;
                }

                return baseSpeed;
            }
        }

        [Button]
        public void AddAdditiveModifier(float modifier)
        {
            _additiveModifiers.Add(modifier);
            InvokeOnChanged();
        }

        [Button]
        public void RemoveAdditiveModifier(float modifier)
        {
            _additiveModifiers.Remove(modifier);
            InvokeOnChanged();
        }

        [Button]
        public void AddMultiplicativeModifier(float multiplier)
        {
            _multiplicativeModifiers.Add(multiplier);
            InvokeOnChanged();
        }

        [Button]
        public void RemoveMultiplicativeModifier(float multiplier)
        {
            _multiplicativeModifiers.Remove(multiplier);
            InvokeOnChanged();
        }

        [Button]
        public void ClearModifiers()
        {
            _additiveModifiers.Clear();
            _multiplicativeModifiers.Clear();
            InvokeOnChanged();
        }

        private void InvokeOnChanged()
        {
            OnChanged?.Invoke(FinalSpeed);
        }
    }
}