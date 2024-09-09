using Game.Scripts.UI;
using GameEngine;

namespace Game.Scripts.Gameplay.Character.Main
{
    // ReSharper disable once InconsistentNaming
    public sealed class AccelerationController_MainChar
    {
        private readonly AccelerateMechanics _accelerateMechanics;
        private readonly HudButton _accelerateButton;

        public AccelerationController_MainChar(AccelerateMechanics accelerateMechanics, HudButton accelerateButton)
        {
            _accelerateButton = accelerateButton;
            _accelerateMechanics = accelerateMechanics;
        }

        public void OnEnable()
        {
            _accelerateButton.PointerDown += _accelerateMechanics.ChangeSpeed;
            _accelerateButton.PointerUp += _accelerateMechanics.RevertSpeed;
        }
        
        public void OnDisable()
        {
            _accelerateButton.PointerDown -= _accelerateMechanics.ChangeSpeed;
            _accelerateButton.PointerUp -= _accelerateMechanics.RevertSpeed;
        }
    }
}