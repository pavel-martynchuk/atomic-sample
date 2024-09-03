using Game.Scripts.Character;
using GameEngine;
using UnityEngine;

namespace Game.Gameplay.Character
{
    public sealed class CharacterAnimatorComponent
    {
        private readonly int _death = Animator.StringToHash("Death");
        private readonly int _takeDamage = Animator.StringToHash("TakeDamage");
        private readonly int _dash = Animator.StringToHash("Dash");
        
        private readonly AnimatorTrigger _deathTrigger;
        private readonly AnimatorTrigger<int> _takeDamageTrigger;
        private readonly AnimatorTrigger _dashTrigger;
        
        public CharacterAnimatorComponent(Animator animator, Character_Core core)
        {
            _deathTrigger = new AnimatorTrigger(_death, animator, core.HealthComponent.DeathEvent);
            _takeDamageTrigger = new AnimatorTrigger<int>(_takeDamage, animator, core.HealthComponent.TakeDamageEvent);
            _dashTrigger = new AnimatorTrigger(_dash, animator , core.DashAction.Callback);
        }
        
        public void OnEnable()
        {
            _deathTrigger.OnEnable();
            _takeDamageTrigger.OnEnable();
            _dashTrigger.OnEnable();
        }
        
        public void OnDisable()
        {
            _deathTrigger.OnDisable();
            _takeDamageTrigger.OnDisable();
            _dashTrigger.OnDisable();
        }
    }
}