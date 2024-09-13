using System;
using Atomic.Elements;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class RagdollComponent : IDisposable
    {
        public IAtomicValue<bool> IsActive => _isActive;
        
        private AtomicVariable<bool> _isActive = new(false);

        [SerializeField] private Transform _rootParent;
        [SerializeField] private Rigidbody _rootRigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody[] _rigidbodies;
        [SerializeField] private Transform _core;
        
        private bool IsFrontUp => Vector3.Dot(_hipsBone.up, Vector3.up) > 0;
        private Transform _hipsBone;
        private const string BackStandUpClipName = "Getting Up From Belly";
        private const string FrontStandClipName = "Getting Up From Spine";
        private const int DefaultLayer = -1;
        private Action _standingUpCallback;

        public void Compose()
        {
            _hipsBone = _animator.GetBoneTransform(HumanBodyBones.Hips);
            DisableRagdoll(true);
        }

        [Button]
        public void EnableRagdoll()
        {
            if (_isActive.Value)
                return;

            _rootRigidbody.isKinematic = true;
            _core.SetParent(_hipsBone);
            _animator.enabled = false;
            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = false;
            }
            _isActive.Value = true;
        }
        
        public void DisableRagdoll(bool withFlag)
        {
            if (!_isActive.Value)
                return;
            _animator.enabled = true;
            _core.SetParent(_rootParent);
            _core.localScale = Vector3.one;
            _core.DOLocalMove(Vector3.zero, 1f);
            _core.DOLocalRotate(Vector3.zero, 1f);
            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = true;
            }
            _rootRigidbody.isKinematic = !withFlag;
            _isActive.Value = withFlag;
        }
        
        [Button]
        public void PlayStandingUp(Action animationEndedCallback = null)
        {
            if (!_isActive.Value)
                return;
            
            DisableRagdoll(false);
            
            AdjustParentRotationToHipsBone();
            AdjustParentPositionToHipsBone();
            
            _standingUpCallback = animationEndedCallback;

            if (IsFrontUp)
                _animator.Play(FrontStandClipName, DefaultLayer, 0f);
            else
                _animator.Play(BackStandUpClipName, DefaultLayer, 0f);
        }
        
        public void OnStandingUpAnimationEnded()
        {
            _rootRigidbody.isKinematic = false;
            _isActive.Value = false;
            _standingUpCallback?.Invoke();
        }

        private void AdjustParentPositionToHipsBone()
        {
            Vector3 initHipsPosition = _hipsBone.position;
            _rootParent.position = initHipsPosition;

            AdjustParentPositionRelativeGround();

            _hipsBone.position = initHipsPosition;
        }

        private void AdjustParentPositionRelativeGround()
        {
            if (Physics.Raycast(_rootParent.position, Vector3.down, out RaycastHit hit, 5, DefaultLayer))
                _rootParent.position = new Vector3(_rootParent.position.x, hit.point.y, _rootParent.position.z);
        }

        private void AdjustParentRotationToHipsBone()
        {
            Vector3 initHipsPosition = _hipsBone.position;
            Quaternion initHipsRotation = _hipsBone.rotation;

            Vector3 directionForRotate = _hipsBone.up;

            if (IsFrontUp == false)
                directionForRotate *= -1;

            directionForRotate.y = 0;

            Quaternion correctionRotation = Quaternion.FromToRotation(_rootParent.forward, directionForRotate.normalized);
            _rootParent.rotation *= correctionRotation;

            _hipsBone.position = initHipsPosition;
            _hipsBone.rotation = initHipsRotation;
        }

        public void Dispose()
        {
            _isActive?.Dispose();
        }
    }
}