using Atomic.Elements;
using Game.Scripts.GameEngine.UI;
using GameEngine.AtomicObjects;
using GameEngine.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameEngine
{
    public class Weapon : PickupObject
    {
        #region Public API

        public RingSliderFillBar ReloadProgressBar => _reloadProgressBar;
        public Transform Pivot => _pivot;
        public Transform FirePoint => _firePoint;
        public GameObject Projectile => _config.ProjectilePrefab;
        public AmmoType AmmoType => _ammoType;
        public IAtomicVariable<bool> HasAmmo => UpdateAmmoCondition();
        public WeaponConfig Config => _config;

        #endregion
        
        [SerializeField, Required]
        private RingSliderFillBar _reloadProgressBar;
        
        [SerializeField] private bool _composeOnAwake = true;

        [SerializeField, Required] private bool _infinityAmmo = false;
        [SerializeField, Required] private WeaponConfig _config;
        [SerializeField, Required] private Transform _pivot;
        [SerializeField, Required] private Transform _firePoint;
        [SerializeField] private ShotStrategyType _shotStrategyType;
        [SerializeField] private AmmoType _ammoType;

        [SerializeField, ReadOnly, InlineProperty]
        private AtomicVariable<int> _ammoCapacity;

        [SerializeField, ReadOnly, InlineProperty]
        private AtomicVariable<bool> _hasAmmo;
        
        public ShotStrategy ShotStrategy => _shotStrategy;
        private ShotStrategy _shotStrategy;

        private void Awake()
        {
            if (_composeOnAwake)
            {
                Compose();
            }
        }

        protected override void Start()
        {
            base.Start();
            _reloadProgressBar.Hide();
        }

        private void OnEnable()
        {
            _shotStrategy.OnShot.Subscribe(UseAmmo);
        }

        private void OnDisable() => 
            _shotStrategy.OnShot.Unsubscribe(UseAmmo);

        private void UseAmmo() => 
            _ammoCapacity.Value -= 1;
        
        public void Reload() => 
            _ammoCapacity.Value = _config.AmmoCapacity;

        public override void Compose()
        {
            base.Compose();

            _ammoCapacity.Value = _config.AmmoCapacity;
            InitShotStrategy();
        }

        private void InitShotStrategy()
        {
            switch (_shotStrategyType)
            {
                case ShotStrategyType.Single:
                    _shotStrategy = new SingleShot(_config.ProjectilePrefab, _firePoint, _config);
                    break;
                case ShotStrategyType.Buckshot:
                    _shotStrategy = new Buckshot(_config.ProjectilePrefab, _firePoint, _config);
                    break;
                case ShotStrategyType.Ballistic:
                    _shotStrategy = new BallisticShot(_config.ProjectilePrefab, _firePoint, _config);
                    break;
                default:
                    _shotStrategy = new SingleShot(_config.ProjectilePrefab, _firePoint, _config);
                    break;
            }
        }

        public void OnTaken(TargetCaptureMechanics targetCaptureMechanics)
        {
            base.OnTaken();
            if (_shotStrategy is BallisticShot ballisticShot)
            {
                ballisticShot.TargetCaptureMechanics = targetCaptureMechanics;
            }
            Debug.Log($"Weapon - ({gameObject.name}) is TAKEN");
        }
        
        public override void OnDropped()
        {
            base.OnDropped();
            if (_shotStrategy is BallisticShot ballisticShot)
            {
                ballisticShot.TargetCaptureMechanics = null;
            }
            Debug.Log($"Weapon ({gameObject.name}) is DROPPED");
        }
        
        public void RefreshReloadProgress(float value)
        {
            if (value is < 0f or > 1f)
            {
                Debug.LogWarning("Invalid value for - RefreshReloadProgress");
                value = Mathf.Clamp01(value);
            }
            _reloadProgressBar.Refresh(value);
        }

        private IAtomicVariable<bool> UpdateAmmoCondition()
        {
            if (_infinityAmmo)
            {
                _hasAmmo.Value = true;
            }
            else
            {
                _hasAmmo.Value = _ammoCapacity.Value > 0;
            }
            return _hasAmmo;
        }
        
        private enum ShotStrategyType
        {
            Single = 0,
            Buckshot = 1,
            Ballistic = 2,
        }
    }
}