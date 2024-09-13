using System;
using System.Collections;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Infrastructure.Services.Coroutines;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    [Is(ObjectType.Physical)]
    public sealed class RagdollComponent : IDisposable
    {
        public IAtomicValue<bool> IsActive => _isActive;
        
        [ShowInInspector, ReadOnly]
        private AtomicVariable<bool> _isActive = new(false);

        [SerializeField] private Transform _rootParent;
        [SerializeField] private Rigidbody _rootRigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterAnimationEventProxy _eventProxy;
        [SerializeField] private Transform _core;
        [SerializeField] private Rigidbody[] _rigidbodies;
        
        [Get(ObjectAPI.Rigidbody)]
        [SerializeField] private Rigidbody _hipRb;
        
        private bool IsFrontUp => Vector3.Dot(_hipsBone.up, Vector3.up) > 0;
        private Transform _hipsBone;
        private const string BackStandUpClipName = "Getting Up From Belly";
        private const string FrontStandClipName = "Getting Up From Spine";
        private const int DefaultLayer = -1;
        private Action _standingUpCallback;
        private UpdateMechanics _stateController;
        private bool _isStandingInProcess = false;
        private bool _delay = false;
        private ICoroutineRunner _coroutineRunner;

        public void Compose(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _hipsBone = _animator.GetBoneTransform(HumanBodyBones.Hips);
            _stateController = new UpdateMechanics(StateResolve);
            DisableRagdoll(true);
        }

        public void OnEnable()
        {
            _eventProxy.StandingUpAnimationEnded += OnStandingUpAnimationEnded;
        }
        
        public void OnDisable()
        {
            _eventProxy.StandingUpAnimationEnded -= OnStandingUpAnimationEnded;
        }

        public void OnUpdate()
        {
            _stateController.OnUpdate(Time.deltaTime);
        }

        private void StateResolve()
        {
            if (_isActive.Value && !_isStandingInProcess && !_delay)
            {
                if (_rootRigidbody.velocity.magnitude <= 0.1f)
                {
                    PlayStandingUp();
                }
            }
        }

        [Button]
        public void EnableRagdoll()
        {
            if (_isActive.Value)
                return;

            _coroutineRunner.StartCoroutine(Delay());
            _rootRigidbody.isKinematic = true;
            _core.SetParent(_hipsBone);
            _animator.enabled = false;
            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = false;
            }
            _isActive.Value = true;
        }
        
        public void DisableRagdoll(bool isPermanent)
        {
            if (!_isActive.Value)
                return;
            _animator.enabled = true;
            _core.SetParent(_rootParent);
            _core.localScale = Vector3.one;
            _core.localPosition = Vector3.zero;
            _core.localRotation = Quaternion.identity;
            foreach (Rigidbody rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = true;
            }
            _rootRigidbody.isKinematic = !isPermanent;
            _isActive.Value = !isPermanent;
        }
        
        [Button]
        public void PlayStandingUp(Action animationEndedCallback = null)
        {
            _isStandingInProcess = true;
            
            if (!_isActive.Value)
                return;
            
            DisableRagdoll(false);
            
            AdjustParentRotationToHipsBone();
            AdjustParentPositionToHipsBone();
            
            _standingUpCallback = animationEndedCallback;

            _animator.Play(IsFrontUp ? FrontStandClipName : BackStandUpClipName, DefaultLayer, 0f);
        }

        IEnumerator Delay()
        {
            _delay = true;
            yield return new WaitForSeconds(3f);
            _delay = false;
        }
        
        private void OnStandingUpAnimationEnded()
        {
            _isStandingInProcess = false;
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