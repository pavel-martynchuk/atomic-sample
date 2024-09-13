using System;

namespace GameEngine.Effects.Application
{
    [Serializable]
    public abstract class EffectApplicationStrategy : IEffectApplicationStrategy
    {
        public abstract void Apply();
    }
}