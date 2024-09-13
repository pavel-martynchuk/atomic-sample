using System;
using UnityEngine;

namespace GameEngine
{
    public class TriggerObserver : MonoBehaviour
    {
        [SerializeField] private LayerMask _triggerLayerMask;

        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (IsInTriggerLayerMask(other.gameObject))
            {
                TriggerEnter?.Invoke(other);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (IsInTriggerLayerMask(other.gameObject))
            {
                TriggerExit?.Invoke(other);
            }
        }
        
        private bool IsInTriggerLayerMask(GameObject obj) =>
            (_triggerLayerMask.value & (1 << obj.layer)) > 0;
    }
}