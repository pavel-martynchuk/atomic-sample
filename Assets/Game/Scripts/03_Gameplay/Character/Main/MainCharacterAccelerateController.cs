using Game.Scripts.UI;
using GameEngine;

namespace Game.Scripts.Gameplay.Character.Main
{
    public sealed class MainCharacterAccelerateController
    {
        private readonly AccelerateMechanics _accelerateMechanics;
        private readonly HudButton _accelerateButton;

        public MainCharacterAccelerateController(AccelerateMechanics accelerateMechanics, HudButton accelerateButton)
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
            _accelerateButton.PointerDown += _accelerateMechanics.ChangeSpeed;
            _accelerateButton.PointerUp += _accelerateMechanics.RevertSpeed;
        }
    }
}