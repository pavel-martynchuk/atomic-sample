using System;
using Atomic.Elements;
using Sirenix.OdinInspector;

namespace GameEngine
{
    [Serializable]
    public sealed class FireAction : IAtomicAction
    {
        private IAtomicVariable<int> _charges;
        private IAtomicValue<bool> _shootCondition;
        private IAtomicAction _shootAction;

        public void Compose(
            IAtomicAction shootAction,
            IAtomicVariable<int> charges,
            IAtomicValue<bool> shootCondition
        )
        {
            _shootAction = shootAction;
            _charges = charges;
            _shootCondition = shootCondition;
        }

        [Button]
        public void Invoke()
        {
            if (!_shootCondition.Value)
                return;

            _shootAction.Invoke();
            _charges.Value--;
        }
    }
}