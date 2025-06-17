using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Misc/Zombie Spawner Difficulties")]
    public class ZombieSpawnerDifficulties : ScriptableObject
    {
        [SerializeField] private int[] _zombiesPerDifficulty;

        public int GetZombiesAmount(int difficulty)
        {
            if (difficulty < 0)
            {
                return _zombiesPerDifficulty[0];
            }
            if (difficulty >= _zombiesPerDifficulty.Length)
            {
                return _zombiesPerDifficulty[^1];
            }
            return _zombiesPerDifficulty[difficulty];
        }
    }
}
