using System.Collections;
using Atomic.Objects;
using GameEngine.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay.Traps
{
    public class Geyser : Trap
    {
        [SerializeField] [BoxGroup("ThrowbackEffect"), HideLabel]
        private ThrowbackEffect _throwbackEffect;

        [SerializeField] private float _geyserInterval = 20f; 
        [SerializeField] private float _checkRadius = 5f;    
        [SerializeField] private LayerMask _targetLayer;
        
        private Coroutine _geyserCoroutine;
        
        protected new void OnEnable()
        {
            base.OnEnable();
            _geyserCoroutine = StartCoroutine(GeyserRoutine());
        }

        protected new void OnDisable()
        {
            base.OnDisable();
            if (_geyserCoroutine != null)
            {
                StopCoroutine(_geyserCoroutine);
            }
        }

        private IEnumerator GeyserRoutine()
        {
            while (true)
            {
                CheckAnim();
                CheckAndThrowTargets();
                yield return new WaitForSeconds(_geyserInterval);
            }
        }

        private void CheckAndThrowTargets()
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, _checkRadius, _targetLayer);

            foreach (var targetCollider in targets)
            {
                AtomicObject atomicObject = targetCollider.GetComponentInParent<AtomicObject>();

                if (atomicObject != null)
                {
                    _throwbackEffect.ApplyEffect(atomicObject);
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, _checkRadius);
        }
    }
}
