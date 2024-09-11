using Atomic.Objects;
using GameEngine.AtomicObjects;
using GameEngine.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    public class Weapon : PickupObject
    {
        public Transform Pivot => _pivot;
        public Transform FirePoint => _firePoint;
        public AtomicObject Projectile => _projectile;

        [SerializeField, Required]
        private WeaponConfig _config;

        [SerializeField, Required]
        private Transform _pivot;
        
        [SerializeField, Required]
        private Transform _firePoint;
        
        [SerializeField, Required]
        private AtomicObject _projectile;

        protected override void Use()
        {
            base.Use();
            Debug.LogError("Use - Weapon");
        }
    }
}