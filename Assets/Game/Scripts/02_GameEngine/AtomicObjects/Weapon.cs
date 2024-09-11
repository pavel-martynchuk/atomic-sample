using Atomic.Elements;
using GameEngine.AtomicObjects;
using GameEngine.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    public class Weapon : PickupObject
    {
        #region Public API

        public Transform Pivot => _pivot;
        public Transform FirePoint => _firePoint;
        public GameObject Projectile => _config.ProjectilePrefab;

        #endregion
        
        [SerializeField] private bool _composeOnAwake = true;

        [SerializeField, Required] private WeaponConfig _config;
        [SerializeField, Required] private Transform _pivot;
        [SerializeField, Required] private Transform _firePoint;
        [SerializeField] private ShotStrategyType _shotStrategyType;
        [SerializeField] private AtomicVariable<Transform> _target;

        public ShotStrategy ShotStrategy => _shotStrategy;
        private ShotStrategy _shotStrategy;

        private void Awake()
        {
            if (_composeOnAwake)
            {
                Compose();
            }
        }

        public override void Compose()
        {
            base.Compose();

            switch (_shotStrategyType)
            {
                case ShotStrategyType.Single:
                    _shotStrategy = new SingleShot(_config.ProjectilePrefab, _firePoint, _config);
                    break;
                case ShotStrategyType.Buckshot:
                    _shotStrategy = new Buckshot(_config.ProjectilePrefab, _firePoint, _config);
                    break;
                case ShotStrategyType.Ballistic:
                    _shotStrategy = new BallisticShot(_config.ProjectilePrefab, _firePoint, _config, _target);
                    break;
                default:
                    _shotStrategy = new SingleShot(_config.ProjectilePrefab, _firePoint, _config);
                    break;
            }
        }

        protected override void Use()
        {
            base.Use();
            Debug.LogError("Use - Weapon");
        }

        private enum ShotStrategyType
        {
            Single = 0,
            Buckshot = 1,
            Ballistic = 2,
        }
    }
}