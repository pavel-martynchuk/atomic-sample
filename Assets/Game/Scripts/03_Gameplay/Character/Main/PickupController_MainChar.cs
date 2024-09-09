using Game.Scripts.UI;
using GameEngine;

namespace Game.Scripts.Gameplay.Character.Main
{
    // ReSharper disable once InconsistentNaming
    public class PickupController_MainChar
    {
        private readonly PickupMechanics _pickupMechanics;
        private readonly HudButton _useButton;

        public PickupController_MainChar(
            PickupMechanics pickupMechanics,
            HudButton useButton)
        {
            _pickupMechanics = pickupMechanics;
            _useButton = useButton;
        }

        public void Init()
        {
            ResetUseButton();
        }

        public void OnEnable()
        {
            _pickupMechanics..Callback.Subscribe(OnPickupComplete);
            _pickupMechanics.TriggerEnterAction.;
            _pickupMechanics.TriggerExit.Subscribe( OnTriggerExit);
            _useButton.PointerDown += _pickupMechanics.StartProcessing;
            _useButton.PointerUp += _pickupMechanics.EndProcessing;
        }
        
        public void OnDisable()
        {
            _pickupAction.Callback.Unsubscribe(OnPickupComplete);
            _pickupMechanics.TriggerEnter.Unsubscribe(OnTriggerEnter);
            _pickupMechanics.TriggerExit.Unsubscribe( OnTriggerExit);
            _useButton.PointerDown -= _pickupMechanics.StartProcessing;
            _useButton.PointerUp -= _pickupMechanics.EndProcessing;
        }

        private void OnTriggerEnter()
        {
            _useButton.Show();
        }

        private void OnTriggerExit()
        {
            ResetUseButton();
        }

        private void OnPickupComplete()
        {
            _useButton.ResetButtonPress();
            _useButton.Hide();
        }

        private void ResetUseButton()
        {
            _useButton.ResetButtonPress();
            _useButton.Hide();
        }
    }
}