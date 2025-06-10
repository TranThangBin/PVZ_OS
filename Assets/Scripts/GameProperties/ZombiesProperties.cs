using System;

namespace Game
{
    [Serializable]
    public struct ZombiesProperties
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
