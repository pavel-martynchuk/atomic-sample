using Atomic.Behaviours;
using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class MoveAnimatorController : IUpdate
    {
        private readonly int IsMoving = Animator.StringToHash("IsMoving");

        private readonly Animator _animator;
        private readonly IAtomicValue<bool> _isMoving;

        public MoveAnimatorController(Animator animator, IAtomicValue<bool> isMoving)
        {
            _animator = animator;
            _isMoving = isMoving;
        }

        public void OnUpdate(float deltaTime)
        {
            _animator.SetBool(IsMoving, _isMoving.Value);
        }
    }
}