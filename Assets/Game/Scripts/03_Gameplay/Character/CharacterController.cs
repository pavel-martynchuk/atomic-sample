using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using GameEngine;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private AtomicObject _character;
    [SerializeField] private Button _dashButton;
    
    private InputController _inputController;
    private DashController _dashController;
    
    private void Start()
    {
        IAtomicVariable<Vector3> moveDirection = _character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
        IAtomicVariable<Vector3> rotateDirection = _character.GetVariable<Vector3>(ObjectAPI.RotateDirection);
        IAtomicAction dashAction = _character.GetAction(ObjectAPI.DashAction);
        
        _inputController = new InputController(new []{moveDirection, rotateDirection});
        _dashController = new DashController(dashAction, _dashButton);

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
    }
    
    private void Unsubscribe()
    {
        _dashController.OnDisable();
    }
    
}