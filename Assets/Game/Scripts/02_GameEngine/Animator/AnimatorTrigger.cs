using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public class AnimatorTrigger
    {
        private readonly int _hash;

        private readonly Animator _animator;
        private readonly IAtomicObservable _animatorEvent;

        public AnimatorTrigger(int hash, Animator animator, IAtomicObservable animatorEvent)
        {
            _hash = hash;
            _animator = animator;
            _animatorEvent = animatorEvent;
        }

        public void OnEnable()
        {
            _animatorEvent.Subscribe(Trigger);
        }

        public void OnDisable()
        {
            _animatorEvent.Unsubscribe(Trigger);
        }
        
        private void Trigger()
        {
            _animator.SetTrigger(_hash);
        }
    }
    
    public sealed class AnimatorTrigger<T> 
    {
        private readonly int _hash;

        private readonly Animator _animator;
        private readonly IAtomicObservable<T> _animatorEvent;

        public AnimatorTrigger(int hash, Animator animator, IAtomicObservable<T> animatorEvent)
        {
            _hash = hash;
            _animator = animator;
            _animatorEvent = animatorEvent;
        }

        public void OnEnable()
        {
            _animatorEvent.Subscribe(Trigger);
        }

        public void OnDisable()
        {
            _animatorEvent.Unsubscribe(Trigger);
        }
        
        private void Trigger(T value)
        {
            _animator.SetTrigger(_hash);
        }
    }
}