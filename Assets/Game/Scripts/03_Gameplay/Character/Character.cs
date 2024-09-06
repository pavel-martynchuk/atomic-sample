using Atomic.Objects;
using Game.Scripts.Gameplay.Character;
using Game.Scripts.Infrastructure.Services.Coroutines;
using Game.Scripts.StaticData;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Character
{
    public sealed class Character : AtomicObject, ICoroutineRunner
    {
        [SerializeField] private bool _composeOnAwake = true;
        [SerializeField, Required] private CharacterStaticData _staticDataConfig;
        
        [SerializeField]
        [BoxGroup("Data")]
        private Character_Data _data;
        
        [Section]
        [SerializeField]
        [BoxGroup("Core")]
        private Character_Core _core;

        [SerializeField]
        [BoxGroup("View")]
        private Character_View _view;

        
        public override void Compose()
        {
            base.Compose();
            
            _data.Compose(_staticDataConfig);
            _core.Compose(this, _data);
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
            _data.Dispose();
            _core.Dispose();
            _view.Dispose();
        }
    }
}