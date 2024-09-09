using System;
using Atomic.Elements;

namespace GameEngine
{
    [Serializable]
    public class PickupAction : IAtomicAction, IDisposable
    {
        public IAtomicObservable Callback => _callback;
        
        private AtomicEvent _callback;
        
        public void Compose()
        {
            _callback = new AtomicEvent();
        }
        
        public void Invoke()
        {
            _callback?.Invoke();
        }

        public void Dispose()
        {
            _callback?.Dispose();
        }
    }
}