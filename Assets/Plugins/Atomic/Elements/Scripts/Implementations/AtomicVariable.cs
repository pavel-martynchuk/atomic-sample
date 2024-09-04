using System;
using Sirenix.OdinInspector;

namespace Atomic.Elements
{
    [Serializable]
    public class AtomicVariable<T> : IAtomicVariable<T>, IAtomicObservable<T>, IDisposable
    {
        [ShowInInspector, HideLabel]
        public virtual T Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                this.OnChanged?.Invoke(value);
            }
        }

        public void Subscribe(Action<T> listener)
        {
            this.OnChanged += listener;
        }

        public void Unsubscribe(Action<T> listener)
        {
            this.OnChanged -= listener;
        }

        protected Action<T> OnChanged;

        [OnValueChanged("OnValueChanged")]
        private T value;

        public AtomicVariable()
        {
            this.value = default;
        }

        public AtomicVariable(T value)
        {
            this.value = value;
        }

#if UNITY_EDITOR
        private void OnValueChanged(T value)
        {
            this.OnChanged?.Invoke(value);
        }
#endif
        public void Dispose()
        {
            AtomicUtils.Dispose(ref this.OnChanged);
        }
    }
}