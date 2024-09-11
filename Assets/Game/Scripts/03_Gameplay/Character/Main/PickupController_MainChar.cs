using Game.Scripts.UI;
using GameEngine;
using GameEngine.AtomicObjects;

namespace Game.Scripts.Gameplay.Character.Main
{
    // ReSharper disable once InconsistentNaming
    public class PickupController_MainChar
    {
        private readonly PickupMechanics _pickupMechanics;
        private readonly HudButton _useButton;

        public PickupController_MainChar(PickupMechanics pickupMechanics, HudButton useButton)
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
            _pickupMechanics.TriggerEnterEvent.Subscribe(OnTriggerEnterWithPickup);
            _pickupMechanics.TriggerExitEvent.Subscribe(OnTriggerExitWithPickup);
            
            _useButton.PointerDown += _pickupMechanics.StartPickingUp;
            _useButton.PointerUp += _pickupMechanics.StopPickingUp;
            
            _pickupMechanics.PickingUpCompleteEvent.Subscribe(OnPickingUpComplete);
        }
        
        public void OnDisable()
        {
            _pickupMechanics.TriggerEnterEvent.Unsubscribe(OnTriggerEnterWithPickup);
            _pickupMechanics.TriggerExitEvent.Unsubscribe(OnTriggerExitWithPickup);
            
            _useButton.PointerDown -= _pickupMechanics.StartPickingUp;
            _useButton.PointerUp -= _pickupMechanics.StopPickingUp;
            
            _pickupMechanics.PickingUpCompleteEvent.Unsubscribe(OnPickingUpComplete);
        }

        private void OnTriggerEnterWithPickup()
        {
            _useButton.Show();
        }

        private void OnTriggerExitWithPickup()
        {
            _useButton.Hide();
        }

        private void OnPickingUpComplete(PickupObject pickupObject)
        {
            ResetUseButton();
        }

        private void ResetUseButton()
        {
            _useButton.ResetButtonPress();
            _useButton.Hide();
        }
    }
}