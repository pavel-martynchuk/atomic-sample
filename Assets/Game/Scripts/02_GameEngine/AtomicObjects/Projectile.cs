using Atomic.Behaviours;
using UnityEngine;

namespace GameEngine
{
    public class Projectile : AtomicBehaviour
    {
        [SerializeField]
        private bool _composeOnAwake = true;

        public override void Compose()
        {
            base.Compose();
        }

        private void Awake()
        {
            if (_composeOnAwake)
            {
                Compose();
            }
        }
    }
}