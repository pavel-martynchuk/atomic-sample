using System;
using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class FireComponent : IDisposable
    {
        public AtomicVariable<bool> FireEnable = new(true);
        
        [Get(ObjectAPI.FireAction)]
        public FireAction FireAction;
        public AtomicEvent FireEvent;

        public Transform FirePoint;
        public AtomicObject BulletPrefab;
        public AtomicVariable<int> Charges = new(10);

        private AndExpression _fireCondition;
        public SpawnBulletAction SpawnBulletAction;
        
        public void Compose()
        {
            _fireCondition.Append(FireEnable);
            _fireCondition.Append(Charges.AsFunction(it => it.Value > 0));
            
            FireAction.Compose(SpawnBulletAction, Charges, _fireCondition, FireEvent);
            SpawnBulletAction.Compose(FirePoint, BulletPrefab);
        }

        public void Dispose()
        {
            FireEvent?.Dispose();
            Charges?.Dispose();
        }
    }
}