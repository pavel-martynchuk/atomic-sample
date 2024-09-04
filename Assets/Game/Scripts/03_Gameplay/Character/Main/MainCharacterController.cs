using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Scripts.UI;
using GameEngine;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Gameplay.Character.Main
{
    public class MainCharacterController : MonoBehaviour
    {
        [SerializeField] private AtomicObject _character;
        [SerializeField] private Button _dashButton;
        [SerializeField] private HudButton  _accelerateButton;
    
        private InputController _inputController;
        private MainCharacterDashController _dashController;
        private MainCharacterAccelerateController _accelerateController;
    
        private void Start()
        {
            IAtomicVariable<Vector3> moveDirection = _character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            IAtomicVariable<Vector3> rotateDirection = _character.GetVariable<Vector3>(ObjectAPI.RotateDirection);
            IAtomicAction dashAction = _character.GetAction(ObjectAPI.DashAction);
            AccelerateMechanics accelerateMechanics = _character.Get<AccelerateMechanics>(ObjectAPI.AccelerateMechanics);
        
            _inputController = new InputController(new []{moveDirection, rotateDirection});
            _dashController = new MainCharacterDashController(dashAction, _dashButton);
            _dashController = new MainCharacterDashController(dashAction, _dashButton);
            _accelerateController = new MainCharacterAccelerateController(accelerateMechanics, _accelerateButton);

            Subscribe();
        }
    
        private void Update()
        {
            _inputController.Update();
            _dashController.Update();
        }
    
        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _dashController.OnEnable();
            _accelerateController.OnEnable();
        }
    
        private void Unsubscribe()
        {
            _dashController.OnDisable();
            _accelerateController.OnDisable();
        }
    
    }
}