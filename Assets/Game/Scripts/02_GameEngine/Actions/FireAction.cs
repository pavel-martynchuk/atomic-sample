using System;
using Atomic.Elements;
using Sirenix.OdinInspector;

namespace GameEngine
{
    [Serializable]
    public sealed class FireAction : IAtomicAction
    {
        private IAtomicValue<bool> _shootCondition;
        private ShotStrategy _shotStrategy;

        public void Compose(IAtomicValue<bool> shootCondition, ShotStrategy shotStrategy)
        {
            _shootCondition = shootCondition;
            _shotStrategy = shotStrategy;
        }

        [Button]
        public void Invoke()
        {
            if (!_shootCondition.Value)
                return;

            _shotStrategy.Shot();
        }
    }
}