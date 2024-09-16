using System;
using Atomic.Objects;
using GameEngine;
using GameEngine.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Traps
{
    public class Trampoline : Trap
    {
        [SerializeField] private Transform _directionVisual;


        [SerializeField] [BoxGroup("ThrowbackEffect"), HideLabel]
        private ThrowbackEffect _throwbackEffect;

        private void Start()
        {
            switch (_throwbackEffect.ThrowDirection)
            {
                case ThrowDirection.Left: _directionVisual.localRotation = Quaternion.Euler(new Vector3(0f, -90f, 0)); break;
                case ThrowDirection.Right: _directionVisual.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0)); break;
                case ThrowDirection.Forward: _directionVisual.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0)); break;
                case ThrowDirection.Back: _directionVisual.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0)); break;
                case ThrowDirection.ToTarget: _directionVisual.gameObject.SetActive(false); break;
            }
        }

        protected override void ApplyEffects(AtomicObject atomicObject)
        {
            if (atomicObject.Is(ObjectType.Physical))
            {
                _throwbackEffect.ApplyEffect(atomicObject);
            }
        }
    }
}