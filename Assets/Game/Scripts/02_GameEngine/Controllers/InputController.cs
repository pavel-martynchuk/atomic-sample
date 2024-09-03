using Atomic.Elements;
using UnityEngine;

namespace GameEngine
{
    public sealed class InputController
    {
        private readonly IAtomicVariable<Vector3>[] _inputDirection;

        public InputController(IAtomicVariable<Vector3>[] inputDirection)
        {
            _inputDirection = inputDirection;
        }

        public void Update()
        {
            foreach (IAtomicVariable<Vector3> direction in _inputDirection)
            {
                direction.Value = GetInputDirection();
            }
        }

        private Vector3 GetInputDirection()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector3 direction = new(horizontalInput, 0, verticalInput);

            return direction;
        }
    }
}