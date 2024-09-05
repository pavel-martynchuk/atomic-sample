using System;
using System.Collections.Generic;
using Atomic.Elements;
using Game.Engine;

namespace GameEngine
{
    [Serializable]
    public sealed class AndExpression : AtomicExpression<bool>
    {
        protected override bool Invoke(IReadOnlyList<IAtomicValue<bool>> members)
        {
            for (int i = 0, count = members.Count; i < count; i++)
            {
                if (!members[i].Value)
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}