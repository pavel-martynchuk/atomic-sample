using Atomic.Objects;
using Game.Scripts._01_Infrastructure.Services.Coroutines;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Character
{
    public sealed class Character : AtomicObject, ICoroutineRunner
    {
        [SerializeField] private bool _composeOnAwake = true;
        
        [Section]
        [SerializeField]
        [BoxGroup("Core")]
        private Character_Core _core;
        
        [Section]
        [SerializeField]
        [BoxGroup("View"), GUIColor(0.8f, 1f, 0.99f)]
        private Character_View _view;

        
        public override void Compose()
        {
            base.Compose();
            
            _core.Compose(this);
            _view.Compose(_core);
        }

        private void Awake()
        {
            if (_composeOnAwake) 
                Compose();
        }

        private void OnEnable()
        {
            _core.OnEnable();
            _view.OnEnable();
        }

        private void Update()
        {
            _core.Update();
            _view.Update();
        }

        private void FixedUpdate()
        {
            _core.FixedUpdate();
        }

        private void OnDisable()
        {
            _core.OnDisable();
            _view.OnDisable();
        }

        private void OnDestroy()
        {
            _core.Dispose();
            _view.Dispose();
        }
    }
}