using System;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class GetHealthMechanics
    {
        private readonly IAtomicValue<int> _maxHealth;
        private readonly IAtomicVariable<int> _currentHealth;
        private readonly IAtomicObservable<int> _getHealthEvent;
        
        public void OnEnable() => 
            _getHealthEvent.Subscribe(OnGetHealth);

        public void OnDisable() => 
            _getHealthEvent.Unsubscribe(OnGetHealth);

        public GetHealthMechanics(
            IAtomicValue<int> maxHealth, 
            IAtomicVariable<int> currentHealth, 
            IAtomicObservable<int> getHealthEvent)
        {
            _maxHealth = maxHealth;
            _currentHealth = currentHealth;
            _getHealthEvent = getHealthEvent;
        }

        private void OnGetHealth(int value)
        {
            if (_currentHealth.Value < _maxHealth.Value)
            {
                _currentHealth.Value += value;
                _currentHealth.Value = Mathf.Clamp(_currentHealth.Value, 0, _maxHealth.Value);
            }
        }
    }
}