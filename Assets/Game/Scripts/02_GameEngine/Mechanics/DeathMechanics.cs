using Atomic.Elements;

namespace GameEngine
{
    public sealed class DeathMechanics
    {
        private readonly IAtomicObservable<int> _health;
        private readonly IAtomicEvent _deathEvent;

        public DeathMechanics(
            IAtomicObservable<int> health,
            IAtomicEvent deathEvent)
        {
            _health = health;
            _deathEvent = deathEvent;
        }

        public void OnEnable() => 
            _health.Subscribe(OnHealthChanged);

        public void OnDisable() => 
            _health.Unsubscribe(OnHealthChanged);

        private void OnHealthChanged(int health)
        {
            if (health <= 0) 
                _deathEvent.Invoke();
        }
    }
}