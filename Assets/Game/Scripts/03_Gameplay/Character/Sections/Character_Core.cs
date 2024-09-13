using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Scripts.Infrastructure.Services.Coroutines;
using GameEngine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Character
{
    // ReSharper disable once InconsistentNaming
    [Serializable]
    public sealed class Character_Core : IDisposable
    {
        [SerializeField, Required] private Rigidbody _rigidbody;
        [SerializeField, Required] private Animator _animator;
        [SerializeField, Required] private Transform _weaponParent;
        [SerializeField, Required] private OrbitAroundParent _orbitAroundParent;

        [SerializeField, Required] private Rigidbody[] _rigidbodies;

        [Section] [BoxGroup("Health Component")]
        public HealthComponent HealthComponent;

        [Section] [BoxGroup("Movement Component")]
        public PhysicalMovementComponent MovementComponent;

        [Section] [BoxGroup("Rotation Component")]
        public PhysicalRotationComponent RotationComponent;

        [Section] [BoxGroup("Fire Component")]
        public FireComponent FireComponent;

        [Section] [Get(ObjectAPI.AccelerateMechanics)] [BoxGroup("Accelerate")]
        public AccelerateMechanics AccelerateMechanics;

        [Section] [Get(ObjectAPI.DashAction)] [BoxGroup("Dash action")]
        public DashAction DashAction;

        [Section] [Get(ObjectAPI.PickupMechanics)][BoxGroup("Pickup")]
        public PickupMechanics PickupMechanics;

        [Section]
        [Get(ObjectAPI.RagdollComponent)] 
        [BoxGroup("Ragdoll")] 
        public RagdollComponent RagdollComponent;

        [BoxGroup("CharacterWeaponComponent")]
        public CharacterWeaponComponent CharacterWeaponComponent;

        [BoxGroup("TargetCaptureMechanics")]
        public TargetCaptureMechanics TargetCaptureMechanics;

        [BoxGroup("CharacterInventoryComponent")]
        public CharacterInventoryComponent CharacterInventoryComponent;

        private UpdateMechanics _stateController;

        public void Compose(ICoroutineRunner coroutineRunner, Character_Data data)
        {
            HealthComponent.Compose(data.Health);
            MovementComponent.Compose(_rigidbody, data.MovementSpeed);
            RotationComponent.Compose(_rigidbody, data.RotationSpeed);
            AccelerateMechanics.Compose(data.MovementSpeed, data.AcceleratedSpeed);
            DashAction.Compose(coroutineRunner, _rigidbody, data.DashDistance, data.DashDuration);
            PickupMechanics.Compose();
            RagdollComponent.Compose(coroutineRunner);
            FireComponent.Compose(CharacterWeaponComponent.CurrentWeapon);
            
            TargetCaptureMechanics.Compose(new AtomicFunction<bool>(() => CharacterWeaponComponent.CurrentWeapon.Value != null));
            
            CharacterWeaponComponent.Compose(
                _rigidbody.transform,
                _weaponParent,
                PickupMechanics.PickingUpCompleteEvent,
                TargetCaptureMechanics,
                CharacterInventoryComponent,
                FireComponent.FireAction,
                coroutineRunner);


            _orbitAroundParent.Compose(TargetCaptureMechanics.CurrentTarget);

            _stateController = new UpdateMechanics(StateResolve);
        }

        public void OnEnable()
        {
            HealthComponent.OnEnable();
            CharacterWeaponComponent.OnEnable();
            PickupMechanics.OnEnable();
            TargetCaptureMechanics.OnEnable();
            RagdollComponent.OnEnable();
        }

        public void OnDisable()
        {
            HealthComponent.OnDisable();
            CharacterWeaponComponent.OnDisable();
            PickupMechanics.OnDisable();
            TargetCaptureMechanics.OnDisable();
            RagdollComponent.OnDisable();
        }

        public void OnUpdate()
        {
            FireComponent.OnUpdate();
            PickupMechanics.OnUpdate();
            TargetCaptureMechanics.OnUpdate();
            RagdollComponent.OnUpdate();
            _stateController.OnUpdate(Time.deltaTime);
        }

        public void OnFixedUpdate()
        {
            MovementComponent.FixedUpdate();
            RotationComponent.OnFixedUpdate();
        }

        private void StateResolve()
        {
            bool isAlive = HealthComponent.IsAlive.Value;
            bool isRagdoll = RagdollComponent.IsActive.Value;

            DashAction.DashEnable.Value = isAlive && !isRagdoll && !DashAction.InProcess.Value;
            MovementComponent.MoveEnable.Value = isAlive && !isRagdoll;
            RotationComponent.RotateEnable.Value = isAlive && !isRagdoll && !DashAction.InProcess.Value;
        }

        public void Dispose()
        {
            HealthComponent?.Dispose();
            MovementComponent?.Dispose();
            FireComponent?.Dispose();
            DashAction?.Dispose();
            PickupMechanics?.Dispose();
            RagdollComponent?.Dispose();
            CharacterWeaponComponent?.Dispose();
        }
    }
}