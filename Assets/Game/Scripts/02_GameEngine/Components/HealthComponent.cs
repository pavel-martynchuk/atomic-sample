using System;
using Atomic.Elements;
using Atomic.Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    [Is(ObjectType.Damageable)]
    public sealed class HealthComponent : IDisposable
    {
        [Get(ObjectAPI.TakeDamageAction)]
        [InlineProperty] public AtomicEvent<int> TakeDamageEvent;
        [InlineProperty] public AtomicEvent<int> GetHealthEvent;
        [InlineProperty] public AtomicEvent DeathEvent;
        public IAtomicValue<bool> IsAlive => _isAlive;

        [Header("Data")]
        [SerializeField, InlineProperty, ReadOnly] private AtomicValue<int> _maxHealth;
        [SerializeField, InlineProperty, ReadOnly] private AtomicVariable<int> _currentHealth;
        [SerializeField, InlineProperty, ReadOnly] private AtomicFunction<bool> _isAlive;
        
        [Header("Mechanics")]
        private TakeDamageMechanics _takeDamageMechanics;
        private GetHealthMechanics _getHealthMechanics;
        private DeathMechanics _deathMechanics;
        
        public void Compose(int maxHealth)
        {
            _maxHealth = new AtomicValue<int>(maxHealth);
            _currentHealth = new AtomicVariable<int>(maxHealth);
            
            _isAlive.Compose(() => _currentHealth.Value > 0);
            
            _takeDamageMechanics = new TakeDamageMechanics(_currentHealth, TakeDamageEvent);
            _getHealthMechanics = new GetHealthMechanics(_maxHealth, _currentHealth, GetHealthEvent);
            _deathMechanics = new DeathMechanics(_currentHealth, DeathEvent);
        }
        
        public void OnEnable()
        {
            _takeDamageMechanics.OnEnable();
            _getHealthMechanics.OnEnable();
            _deathMechanics.OnEnable();
        }

        public void OnDisable()
        {
            _takeDamageMechanics.OnDisable();
            _getHealthMechanics.OnDisable();
            _deathMechanics.OnDisable();
        }
        
        public void Dispose()
        {
            _currentHealth?.Dispose();
            DeathEvent?.Dispose();
            TakeDamageEvent?.Dispose();
            GetHealthEvent?.Dispose();
        }
    }
}