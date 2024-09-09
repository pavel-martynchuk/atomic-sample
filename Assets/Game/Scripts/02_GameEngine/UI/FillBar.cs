using UnityEngine;

namespace Game.Scripts.GameEngine.UI
{
    public abstract class FillBar : MonoBehaviour
    {
        public abstract void Init(bool isFilled);

        public abstract void Refresh(float clampedValue);
    }
}