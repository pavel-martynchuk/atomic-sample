using System;
using Game.Gameplay.Character;
using GameEngine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Character
{
    // ReSharper disable once InconsistentNaming
    [Serializable]
    public class Character_View : IDisposable
    {
        [SerializeField, Required]
        private Animator _animator;

        private CharacterAnimatorComponent _animatorComponent;
        private MoveAnimatorController _moveAnimatorController;
        
        public void Compose(Character_Core core)
        {
            _moveAnimatorController = new MoveAnimatorController(_animator, core.MovementComponent.IsMoving);
            _animatorComponent = new CharacterAnimatorComponent(_animator, core);
        }
        
        public void OnEnable()
        {
            _animatorComponent.OnEnable();
        }

        public void Update()
        {
            _moveAnimatorController.OnUpdate(Time.deltaTime);
        }
        
        public void OnDisable()
        {
            _animatorComponent.OnDisable();
        }


        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}