using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts._01_Infrastructure.Services.Coroutines;
using Game.Scripts.StaticData;
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
        [SerializeField, Required] private CharacterStaticData _staticDataConfig;

        [SerializeField]
        [PropertySpace(SpaceBefore = 50, SpaceAfter = 50)]
        private AtomicVariable<bool> _isRagdoll = new(false);

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
        
        private UpdateMechanics _stateController;
        
        public void Compose(ICoroutineRunner coroutineRunner)
        {
            HealthComponent.Compose(_staticDataConfig.Health);

            AtomicVariable<float> speed = new(_staticDataConfig.Speed);
            AtomicValue<float> dashDistance = new(_staticDataConfig.DashDistance);
            AtomicValue<float> dashDuration = new(_staticDataConfig.DashDuration);
            
            MovementComponent.Compose(_rigidbody, speed);
            RotationComponent.Compose(_rigidbody, speed);
            DashAction.Compose(coroutineRunner, _rigidbody, dashDistance, dashDuration);

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
            _isRagdoll?.Dispose();
            HealthComponent?.Dispose();
            MovementComponent?.Dispose();
            DashAction?.Dispose();
        }

        private void StateResolve()
        {
            bool isAlive = HealthComponent.IsAlive.Value;
            bool isRagdoll = _isRagdoll.Value;
                
            
            DashAction.DashEnable.Value = isAlive && !isRagdoll && !DashAction.InProcess.Value;
            MovementComponent.MoveEnable.Value = isAlive && !isRagdoll;
            RotationComponent.RotateEnable.Value = isAlive && !isRagdoll && !DashAction.InProcess.Value;
        }
    }
}