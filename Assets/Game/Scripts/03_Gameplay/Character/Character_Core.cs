using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts._01_Infrastructure.Services.Coroutines;
using Game.Scripts._03_Gameplay.Character;
using GameEngine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Character
{
    // ReSharper disable once InconsistentNaming
    [Serializable]
    public sealed class Character_Core : IDisposable
    {
        [SerializeField, Required] private Rigidbody _rigidbody;
        [SerializeField, Required] private Animator _animator;
        [SerializeField, Required] private Rigidbody[] _rigidbodies;
        
        [Section]
        [BoxGroup("Health Component"), HideLabel]
        public HealthComponent HealthComponent;
        
        [Section]
        [BoxGroup("Movement Component"), HideLabel]
        public PhysicalMovementComponent MovementComponent;
        
        [Section]
        [BoxGroup("Rotation Component"), HideLabel]
        public PhysicalRotationComponent RotationComponent;

        [Get(ObjectAPI.DashAction), Section]
        [BoxGroup("Dash action"), HideLabel]
        public DashAction DashAction;
        
        [BoxGroup("Ragdoll"), HideLabel]
        public Ragdoll Ragdoll;
        
        private UpdateMechanics _stateController;
        
        public void Compose(ICoroutineRunner coroutineRunner, Character_Data data)
        {
            HealthComponent.Compose(data.Health);
            MovementComponent.Compose(_rigidbody, data.MovementSpeed);
            RotationComponent.Compose(_rigidbody, data.RotationSpeed);
            DashAction.Compose(coroutineRunner, _rigidbody, data.DashDistance, data.DashDuration);
            Ragdoll.Compose(_animator, _rigidbodies);
            
            _stateController = new UpdateMechanics(StateResolve);
        }

        public void OnEnable()
        {
            HealthComponent.OnEnable();
        }

        public void Update()
        {
            _stateController.OnUpdate(Time.deltaTime);
        }

        public void FixedUpdate()
        {
            MovementComponent.FixedUpdate(); 
            RotationComponent.FixedUpdate(); 
        }

        public void OnDisable()
        {
            HealthComponent.OnDisable();
        }

        public void Dispose()
        {
            Ragdoll?.Dispose();
            HealthComponent?.Dispose();
            MovementComponent?.Dispose();
            DashAction?.Dispose();
        }

        private void StateResolve()
        {
            bool isAlive = HealthComponent.IsAlive.Value;
            bool isRagdoll = Ragdoll.IsActive.Value;
            
            DashAction.DashEnable.Value = isAlive && !isRagdoll && !DashAction.InProcess.Value;
            MovementComponent.MoveEnable.Value = isAlive && !isRagdoll;
            RotationComponent.RotateEnable.Value = isAlive && !isRagdoll && !DashAction.InProcess.Value;
        }
    }
}