using System;
using Atomic.Elements;

namespace GameEngine
{
    [Serializable]
    public sealed class TakeDamageMechanics
    {
        private readonly IAtomicVariable<int> _health;
        private readonly IAtomicObservable<int> _takeDamageEvent;

        public TakeDamageMechanics(
            IAtomicVariable<int> health,
            AtomicEvent<int> takeDamageEvent)
        {
            _health = health;
            _takeDamageEvent = takeDamageEvent;
        }

        public void OnEnable() => 
            _takeDamageEvent.Subscribe(OnTakeDamage);

        public void OnDisable() => 
            _takeDamageEvent.Unsubscribe(OnTakeDamage);

        private void OnTakeDamage(int damage)
        {
            if (_health.Value > 0)
            {
                _health.Value = Math.Max(0, _health.Value - damage);
            }
        }
    }
}