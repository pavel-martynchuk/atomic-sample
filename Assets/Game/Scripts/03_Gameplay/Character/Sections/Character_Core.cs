using System;
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
        [SerializeField, Required] private Transform _firePoint;
        [SerializeField, Required] private AtomicObject _bullet;
        [SerializeField, Required] private Transform _weaponParent;
        
        [SerializeField, Required] private Rigidbody[] _rigidbodies;
        
        [Section]
        [BoxGroup("Health Component")]
        public HealthComponent HealthComponent;
        
        [Section]
        [BoxGroup("Movement Component")]
        public PhysicalMovementComponent MovementComponent;
        
        [Section]
        [BoxGroup("Rotation Component")]
        public PhysicalRotationComponent RotationComponent;
        
        [Section]
        [BoxGroup("Fire Component")]
        public FireComponent FireComponent;
        
        [Section]
        [Get(ObjectAPI.AccelerateMechanics)]
        [BoxGroup("Accelerate")] 
        public AccelerateMechanics AccelerateMechanics;

        [Section]
        [Get(ObjectAPI.DashAction)]
        [BoxGroup("Dash action")] 
        public DashAction DashAction;
        
        [Section]
        [Get(ObjectAPI.PickupMechanics)]
        [BoxGroup("Pickup")] 
        public PickupMechanics PickupMechanics;
        
        [BoxGroup("Ragdoll")]
        public Ragdoll Ragdoll;
        
        [BoxGroup("CharacterWeaponComponent")]
        public CharacterWeaponComponent CharacterWeaponComponent;
        
        private UpdateMechanics _stateController;
        
        public void Compose(ICoroutineRunner coroutineRunner, Character_Data data)
        {
            HealthComponent.Compose(data.Health);
            MovementComponent.Compose(_rigidbody, data.MovementSpeed);
            RotationComponent.Compose(_rigidbody, data.RotationSpeed);
            AccelerateMechanics.Compose(data.MovementSpeed, data.AcceleratedSpeed);
            DashAction.Compose(coroutineRunner, _rigidbody, data.DashDistance, data.DashDuration);
            PickupMechanics.Compose();
            FireComponent.Compose(_firePoint, _bullet);
            Ragdoll.Compose(_animator, _rigidbodies);
            CharacterWeaponComponent.Compose(_rigidbody.transform, _weaponParent);

            _stateController = new UpdateMechanics(StateResolve);
        }

        public void OnEnable()
        {
            HealthComponent.OnEnable();
            CharacterWeaponComponent.OnEnable();
        }

        public void OnUpdate()
        {
            PickupMechanics.OnUpdate();
            _stateController.OnUpdate(Time.deltaTime);
        }

        public void OnFixedUpdate()
        {
            MovementComponent.FixedUpdate(); 
            RotationComponent.OnFixedUpdate(); 
        }

        public void OnDisable()
        {
            HealthComponent.OnDisable();
            CharacterWeaponComponent.OnDisable();
        }

        public void OnTriggerEnter(Collider collider) => 
            PickupMechanics.OnTriggerEnter(collider);

        public void OnTriggerExit(Collider collider) => 
            PickupMechanics.OnTriggerExit(collider);

        private void StateResolve()
        {
            bool isAlive = HealthComponent.IsAlive.Value;
            bool isRagdoll = Ragdoll.IsActive.Value;
            
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
            Ragdoll?.Dispose();
            CharacterWeaponComponent?.Dispose();
        }
    }
}