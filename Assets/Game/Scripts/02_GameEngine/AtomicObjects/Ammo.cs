using GameEngine;
using GameEngine.AtomicObjects;
using UnityEngine;

namespace Game.Scripts._02_GameEngine.AtomicObjects
{
    public class Ammo : PickupObject
    {
        public AmmoType AmmoType;
        public GameObject Visual;

        public void Taken()
        {
            TriggerCollider.enabled = false;
            Visual.SetActive(false);
            PickupProgress.Hide();
        }
    }
}