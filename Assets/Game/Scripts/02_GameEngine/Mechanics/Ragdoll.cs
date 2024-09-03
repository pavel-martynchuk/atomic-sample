using System;
using Atomic.Elements;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class Ragdoll : IDisposable
    {
        public IAtomicValue<bool> IsActive => _isActive;
        
        private AtomicVariable<bool> _isActive = new(false);

        private Animator _animator;
        private Rigidbody[] _rigidbodies;

        public void Compose(Animator animator, Rigidbody[] rigidbodies)
        {
            _animator = animator;
            _rigidbodies = rigidbodies;
            Disable();
        }

        [Button]
        public void Enable()
        {
            if (_isActive.Value)
                return;
            
            _animator.enabled = false;
            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = false;
            }
            _isActive.Value = true;
        }
        
        [Button]
        public void Disable()
        {
            if (!_isActive.Value)
                return;
            
            _animator.enabled = true;
            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = true;
            }
            _isActive.Value = false;
        }

        public void Dispose()
        {
            _isActive?.Dispose();
        }
    }
}