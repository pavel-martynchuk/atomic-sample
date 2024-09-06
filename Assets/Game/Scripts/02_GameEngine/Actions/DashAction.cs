using System;
using System.Collections;
using Atomic.Elements;
using Game.Scripts.Infrastructure.Services.Coroutines;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameEngine
{
    [Serializable]
    public class DashAction : IAtomicAction, IDisposable
    {
        [InlineProperty, ReadOnly]
        public AtomicVariable<bool> DashEnable = new(true);

        [InlineProperty, ReadOnly]
        public AtomicVariable<bool> InProcess = new(false);
        
        [InlineProperty, ReadOnly]
        public AtomicEvent Callback;
        
        private ICoroutineRunner _coroutineRunner;
        private Rigidbody _rigidbody;

        [SerializeField, InlineProperty, ReadOnly]
        private AtomicValue<float> _distance;

        [SerializeField, InlineProperty, ReadOnly]
        private AtomicValue<float> _duration;

        public void Compose(
            ICoroutineRunner coroutineRunner,
            Rigidbody rigidbody,
            AtomicValue<float> distance,
            AtomicValue<float> duration)
        {
            _coroutineRunner = coroutineRunner;
            _rigidbody = rigidbody;
            _distance = distance;
            _duration = duration;
        }

        [Button]
        public void Invoke()
        {
            if (DashEnable.Value)
            {
                _coroutineRunner.StartCoroutine(Dash());
            }
        }

        private IEnumerator Dash()
        {
            Callback?.Invoke();
            InProcess.Value = true;
            float elapsedTime = 0f;

            Vector3 startPosition = _rigidbody.position;

            Vector3 targetPosition = startPosition + _rigidbody.transform.forward * _distance.Value;

            while (elapsedTime < _duration.Value)
            {
                float t = elapsedTime / _duration.Value;
                float easeT = Mathf.SmoothStep(0f, 1f, t);
                _rigidbody.MovePosition(Vector3.Lerp(startPosition, targetPosition, easeT));
                elapsedTime += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }

            _rigidbody.MovePosition(targetPosition);
            InProcess.Value = false;
        }


        public void Dispose()
        {
            DashEnable?.Dispose();
            InProcess?.Dispose();
            Callback?.Dispose();
        }
    }
}