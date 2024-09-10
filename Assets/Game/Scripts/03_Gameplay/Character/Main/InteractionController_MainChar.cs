using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Scripts.UI;
using GameEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Gameplay.Character.Main
{
    // ReSharper disable once InconsistentNaming
    public class InteractionController_MainChar : MonoBehaviour
    {
        [SerializeField] private AtomicObject _character;
        [SerializeField] private Button _dashButton;
        [SerializeField] private HudButton  _accelerateButton;
        [SerializeField] private HudButton  _useButton;
    
        private InputController _inputController;
        private DashController_MainChar _dashController;
        private AccelerationController_MainChar _accelerateController;
        private FireController_MainChar _fireController;
        private PickupController_MainChar _pickupController;
    
        private void Start()
        {
            IAtomicVariable<Vector3> moveDirection = _character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            IAtomicVariable<Vector3> rotateDirection = _character.GetVariable<Vector3>(ObjectAPI.RotateDirection);
            
            IAtomicAction dashAction = _character.GetAction(ObjectAPI.DashAction);
            IAtomicAction fireAction = _character.GetAction(ObjectAPI.FireAction);
            
            AccelerateMechanics accelerateMechanics = _character.Get<AccelerateMechanics>(ObjectAPI.AccelerateMechanics);
            PickupMechanics pickupMechanics = _character.Get<PickupMechanics>(ObjectAPI.PickupMechanics);
        
            _inputController = new InputController(new []{moveDirection, rotateDirection});
            _dashController = new DashController_MainChar(dashAction, _dashButton);
            _dashController = new DashController_MainChar(dashAction, _dashButton);
            _accelerateController = new AccelerationController_MainChar(accelerateMechanics, _accelerateButton);
            _fireController = new FireController_MainChar(fireAction);
            _pickupController = new PickupController_MainChar(pickupMechanics, _useButton);

            
            _pickupController.Init();
            
            Subscribe();
        }
    
        private void Update()
        {
            _inputController.Update();
            _dashController.Update();
            _fireController.Update();
        }
    
        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _dashController.OnEnable();
            _accelerateController.OnEnable();
            _pickupController.OnEnable();
        }
    
        private void Unsubscribe()
        {
            _dashController.OnDisable();
            _accelerateController.OnDisable();
            _pickupController.OnDisable();
        }
    
    }
}