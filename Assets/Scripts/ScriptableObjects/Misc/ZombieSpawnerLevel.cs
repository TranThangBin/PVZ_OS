using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Misc/Zombie Spawner Props")]
    public class ZombieSpawnerLevel : ScriptableObject
    {
        [field: SerializeField] public float ZombieSpawnInterval { get; private set; }
        [field: SerializeField] public int Waves { get; private set; }
        [field: SerializeField] public int MinDifficulty { get; private set; }
        [field: SerializeField] public int DifficultyIncrement { get; private set; }
        [field: SerializeField] public BasicZombie BasicZombie { get; private set; }
        [field: SerializeField] public Trophy Trophy { get; private set; }
        [field: SerializeField] public ZombieSpawnerDifficulties Difficulties { get; private set; }
    }
}
