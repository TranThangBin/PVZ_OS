using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Spawner Properties")]
    public class SpawnersProperties : ScriptableObject
    {
        public ZombieSpanwerProperties ZombieSpawner;
        public SunSpanwerProperties SunSpanwer;
    }

    [Serializable]
    public struct ZombieSpanwerProperties
    {
        public float SpawnTime;
        public int RowCount;
        public BasicZombie BasicZombie;
    }

    [Serializable]
    public struct SunSpanwerProperties
    {
        public float SpawnTime;
        public Vector2 Padding;
        public Sun Sun;
    }
}
