using GameEngine.Interfaces;
using UnityEngine;

namespace Game.Scripts._02_GameEngine.AtomicObjects
{
    public class Enemy : MonoBehaviour, ITargeted
    {
        public Vector3 GetPosition() => transform.position;
    }
}