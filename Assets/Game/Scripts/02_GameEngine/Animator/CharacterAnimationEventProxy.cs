using System;
using UnityEngine;

namespace GameEngine
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimationEventProxy : MonoBehaviour
    {
        public event Action StandingUpAnimationEnded;
        
        public void OnStandingUpAnimationEnded()
        {
            Debug.LogError(2);
            StandingUpAnimationEnded?.Invoke();
        }
    }
}
