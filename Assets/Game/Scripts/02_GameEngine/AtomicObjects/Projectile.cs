using Atomic.Behaviours;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    public class Projectile : AtomicBehaviour
    {
        [SerializeField]
        private bool _composeOnAwake = true;
        
        [SerializeField]
        private float _speed = 50f;
        
        [Section]
        [SerializeField]
        private MovementComponent _movementComponent;

        public override void Compose()
        {
            base.Compose();
            _movementComponent.Compose(transform, new AtomicVariable<float>(_speed));
        }

        private void Awake()
        {
            if (_composeOnAwake)
            {
                Compose();
            }
        }

        protected override void Update()
        {
            base.Update();
            _movementComponent.OnUpdate();
        }

        private void OnDestroy()
        {
            _movementComponent.Dispose();
        }
    }
}