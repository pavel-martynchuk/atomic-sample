using UnityEngine;

namespace Game.Scripts.StaticData
{
    [CreateAssetMenu(menuName = "StaticData/CharacterStaticData", fileName = "CharacterStaticData")]
    public class CharacterStaticData : ScriptableObject
    {
        [Header("Stats")]
        public int Health;
        
        [Header("Movement")]
        public float Speed;
        public float DashDistance;
        public float DashDuration;
    }
}