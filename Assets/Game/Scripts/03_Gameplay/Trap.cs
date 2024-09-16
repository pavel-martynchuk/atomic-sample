using System.Collections;
using System.Collections.Generic;
using Atomic.Objects;
using GameEngine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class Trap : AtomicObject
    {
        [SerializeField] protected List<ActivateConditions> Conditions;

        [SerializeField, ShowIf("HasTriggerStayCondition")]
        private float _stayInterval = 1f;

        [SerializeField] protected bool IsRemoveOnExit;

        [SerializeField, Required] protected TriggerObserver TriggerObserver;

        [SerializeField] protected Animation Animation;

        private bool _isInTrigger = false;
        private Coroutine _stayCoroutine;

        protected void OnEnable()
        {
            TriggerObserver.TriggerEnter += OnTriggerEnter;
            TriggerObserver.TriggerExit += OnTriggerExit;
        }

        protected void OnDisable()
        {
            TriggerObserver.TriggerEnter -= OnTriggerEnter;
            TriggerObserver.TriggerExit -= OnTriggerExit;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            foreach (ActivateConditions condition in Conditions)
            {
                if (condition == ActivateConditions.TriggerEnter)
                {
                    Activate(other);
                }

                if (condition == ActivateConditions.TriggerStay)
                {
                    if (_stayCoroutine == null)
                    {
                        _isInTrigger = true;
                        _stayCoroutine = StartCoroutine(ActivateAtInterval(other));
                    }
                }
            }
        }

        private IEnumerator ActivateAtInterval(Collider target)
        {
            while (_isInTrigger)
            {
                Activate(target);
                yield return new WaitForSeconds(_stayInterval);
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            _isInTrigger = false;

            if (_stayCoroutine != null)
            {
                StopCoroutine(_stayCoroutine);
                _stayCoroutine = null;
            }

            if (IsRemoveOnExit)
            {
                AtomicObject atomicObject = other.GetComponentInParent<AtomicObject>();
                if (atomicObject)
                    RemoveEffects(atomicObject);
            }
        }

        protected virtual void Activate(Collider target)
        {
            AtomicObject atomicObject = target.GetComponentInParent<AtomicObject>();
            if (atomicObject)
                ApplyEffects(atomicObject);

            CheckAnim();
        }

        protected void CheckAnim()
        {
            if (Animation != null)
            {
                Animation.Play();
            }
        }

        protected virtual void ApplyEffects(AtomicObject atomicObject)
        {
        }

        protected virtual void RemoveEffects(AtomicObject atomicObject)
        {
        }
        
        private bool HasTriggerStayCondition() =>
            Conditions != null && Conditions.Contains(ActivateConditions.TriggerStay);
    }

    public enum ActivateConditions
    {
        TriggerEnter = 0,
        TriggerStay = 1,
        Processing = 2,
        Projectile = 3,
        Explosion = 4,
        Custom = 5,
    }
}