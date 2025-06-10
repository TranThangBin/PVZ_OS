using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Zombies Properties")]
    public class ZombiesProperties : ScriptableObject
    {
        public BasicZombieProperties BasicZombie;
    }

    [Serializable]
    public struct BasicZombieProperties
    {
        public int Health;
        public float MovementSpeed;
        public int AttackDamage;
        public float AttackRange;
        public float AttackCooldown;
    }
}
