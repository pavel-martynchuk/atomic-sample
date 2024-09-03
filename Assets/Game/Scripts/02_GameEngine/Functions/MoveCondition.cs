using System;
using Atomic.Elements;

namespace GameEngine
{
    [Serializable]
    public sealed class MoveCondition : IAtomicFunction<bool>
    {
        private IAtomicValue<bool> _isAlive;
        private IAtomicValue<bool> _isRagdoll;

        public void Compose(
            IAtomicValue<bool> isAlive, 
            IAtomicValue<bool> isRagdoll)
        {
            _isAlive = isAlive;
            _isRagdoll = isRagdoll;
        }

        public bool Invoke() => 
            _isAlive.Value && !_isRagdoll.Value;
    }
}