using Atomic.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace GameEngine
{
    public sealed class DashController
    {
        private readonly IAtomicAction _dashAction;
        private readonly Button _dashButton;

        public DashController(IAtomicAction dashAction, Button dashButton)
        {
            _dashAction = dashAction;
            _dashButton = dashButton;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
                DoDash();
        }
        
        public void OnEnable() => 
            _dashButton.onClick.AddListener(DoDash);

        public void OnDisable() => 
            _dashButton.onClick.RemoveListener(DoDash);

        private void DoDash() => 
            _dashAction.Invoke();
        
    }
}