using System;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;

namespace GameEngine
{
    [Serializable]
    public sealed class DealDamageAction : IAtomicAction<IAtomicObject>
    {
        private IAtomicValue<int> _damage;

        public void Compose(IAtomicValue<int> damage)
        {
            _damage = damage;
        }

        public void Invoke(IAtomicObject target)
        {
            if (!target.Is(ObjectType.Damageable))
                return;

            IAtomicAction<int> takeDamageAction = target.GetAction<int>(ObjectAPI.TakeDamageAction);
            takeDamageAction?.Invoke(_damage.Value);
        }
    }
}