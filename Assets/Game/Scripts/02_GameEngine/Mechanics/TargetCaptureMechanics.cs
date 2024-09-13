using System;
using System.Collections.Generic;
using Atomic.Elements;
using GameEngine.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class TargetCaptureMechanics
    {
        public IAtomicVariable<Transform> CurrentTarget => _currentTarget;

        [SerializeField, Required] private TriggerObserver _triggerObserver;
        [SerializeField, Required] private GameObject _targetMark;

        [PropertySpace(SpaceBefore = 50f)]
        [ShowInInspector, ReadOnly]
        private List<Transform> _targets = new();

        [SerializeField, ReadOnly, InlineProperty]
        private AtomicVariable<Transform> _currentTarget;

        private bool _isTargetSwitchedManually;
        private IAtomicValue<bool> _hasWeapon;

        public void Compose(IAtomicValue<bool> hasWeapon)
        {
            _hasWeapon = hasWeapon;
            _targets = new List<Transform>();
        }

        public void OnUpdate()
        {
            // Обновляем текущую цель только если она не была принудительно изменена
            if (!_isTargetSwitchedManually)
            {
                UpdateCurrentTargetToClosest();
            }

            UpdateTargetMark();
        }

        public void OnEnable()
        {
            _triggerObserver.TriggerEnter += OnTriggerEnter;
            _triggerObserver.TriggerExit += OnTriggerExit;
        }

        public void OnDisable()
        {
            _triggerObserver.TriggerEnter -= OnTriggerEnter;
            _triggerObserver.TriggerExit -= OnTriggerExit;
        }

        [Button]
        public void SwitchToNextTarget()
        {
            if (_targets.Count == 0) return;

            SortTargetsByDistance();

            int currentIndex = _targets.IndexOf(_currentTarget?.Value);
            if (currentIndex == -1 || currentIndex == _targets.Count - 1)
            {
                _currentTarget.Value = _targets[0]; 
            }
            else
            {
                _currentTarget.Value = _targets[currentIndex + 1]; 
            }

            _isTargetSwitchedManually = true;
        }

        // Проверка и добавление цели при входе в триггер
        private void OnTriggerEnter(Collider collider)
        {
            ITargeted targeted = collider.GetComponentInParent<ITargeted>();
            if (targeted != null)
            {
                _targets.Add(collider.transform);
            }
        }

        // Удаление цели при выходе из триггера
        private void OnTriggerExit(Collider collider)
        {
            ITargeted targeted = collider.GetComponentInParent<ITargeted>();
            if (targeted != null)
            {
                _targets.Remove(collider.transform);

                if (_currentTarget?.Value == collider.transform)
                {
                    _currentTarget.Value = null;
                    _isTargetSwitchedManually = false; 
                    UpdateCurrentTargetToClosest();
                }
            }
        }

        private void UpdateTargetMark()
        {
            bool hasTarget = _currentTarget?.Value != null;
            _targetMark.SetActive(hasTarget && _hasWeapon.Value);

            if (hasTarget && _hasWeapon.Value)
            {
                Vector3 targetPos = _currentTarget.Value.position;
                targetPos.y = 0;
                _targetMark.transform.position = targetPos;
            }
        }

        private void UpdateCurrentTargetToClosest()
        {
            if (_targets.Count == 0)
            {
                _currentTarget.Value = null;
                return;
            }

            Transform closestTarget = FindClosestTarget();
            _currentTarget.Value = closestTarget;
        }

        private Transform FindClosestTarget()
        {
            float closestDistance = float.MaxValue;
            Transform closestTarget = null;

            foreach (Transform target in _targets)
            {
                float distance = Vector3.Distance(_triggerObserver.transform.position, target.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = target;
                }
            }

            return closestTarget;
        }

        private void SortTargetsByDistance()
        {
            _targets.Sort((a, b) =>
                Vector3.Distance(_triggerObserver.transform.position, a.position)
                    .CompareTo(Vector3.Distance(_triggerObserver.transform.position, b.position)));
        }
    }
}
