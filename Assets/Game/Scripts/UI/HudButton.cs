using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class HudButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Button _button;

        public event Action PointerDown;
        public event Action PointerUp;

        private bool _isPointerDown;

        [Button]
        public void Show()
        {
            _button.interactable = true;
        }

        [Button]
        public void Hide()
        {
            _button.interactable = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPointerDown = true;
            PointerDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_button.interactable)
                return;    
            
            _isPointerDown = false;
            PointerUp?.Invoke();
        }

        public void ResetButtonPress()
        {
            if (_isPointerDown)
            {
                _isPointerDown = false;
                PointerUp?.Invoke();

                PointerEventData pointerEventData = new(EventSystem.current);
                ExecuteEvents.Execute(_button.gameObject, pointerEventData, ExecuteEvents.pointerUpHandler);
            }
        }
    }
}
