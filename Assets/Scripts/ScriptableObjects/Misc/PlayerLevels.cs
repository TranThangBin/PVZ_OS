using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Misc/Player Levels")]
    public class PlayerLevels : ScriptableObject
    {
        public int CurrentLevel;
        [field: SerializeField] public ZombieSpawnerLevel[] ZombieSpawnerLevels { get; private set; }
        public ZombieSpawnerLevel CurrentZombieSpawnerLevel =>
            CurrentLevel - 1 >= ZombieSpawnerLevels.Length
            ? null : ZombieSpawnerLevels[CurrentLevel - 1];
        public bool FinalLevel => CurrentLevel - 1 == ZombieSpawnerLevels.Length - 1;
    }
}
