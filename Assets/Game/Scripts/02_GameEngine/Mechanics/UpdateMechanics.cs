using System;
using Atomic.Behaviours;

namespace GameEngine
{
    public sealed class UpdateMechanics : IUpdate
    {
        private readonly Action _action;

        public UpdateMechanics(Action action) => 
            _action = action;

        public void OnUpdate(float deltaTime) => 
            _action?.Invoke();
    }
}