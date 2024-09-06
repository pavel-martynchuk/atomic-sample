using Atomic.Elements;
using UnityEngine;

namespace Game.Scripts.Gameplay.Character.Main
{
    // ReSharper disable once InconsistentNaming
    public sealed class FireController_MainChar
    {
        private readonly IAtomicAction _fireAction;

        public FireController_MainChar(IAtomicAction fireAction)
        {
            _fireAction = fireAction;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
                Fire();
        }

        private void Fire() =>
            _fireAction.Invoke();
    }
}