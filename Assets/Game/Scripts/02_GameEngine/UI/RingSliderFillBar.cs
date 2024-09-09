using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.GameEngine.UI
{
    public class RingSliderFillBar : FillBar
    {
        [SerializeField, Required] private Image _fillRing;
        [SerializeField, Required] private Transform _parent;
        [SerializeField, MinValue(0f)] private float _animDuration = 0.25f;

        private Tween _currentTween;

        private void Awake() => 
            CacheAnimation();

        public void Reset()
        {
            _parent.localScale = Vector3.zero;
            Init(false);
        }

        public void Show()
        {
            _currentTween?.Kill();
            _currentTween = _parent.DOScale(Vector3.one, _animDuration);
        }

        public void Hide()
        {
            _currentTween?.Kill();
            _currentTween = _parent.DOScale(Vector3.zero, _animDuration);
        }

        public override void Init(bool isFilled) => 
            _fillRing.fillAmount = isFilled ? 1f : 0f;

        public override void Refresh(float clampedValue) => 
            _fillRing.fillAmount = clampedValue;
        
        private void CacheAnimation() => 
            _currentTween.SetLink(gameObject).SetUpdate(true);
    }
}