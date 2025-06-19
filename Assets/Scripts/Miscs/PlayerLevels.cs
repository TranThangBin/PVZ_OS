using UnityEngine;

namespace Game
{
    public class PlayerLevels : MonoBehaviour
    {
        [SerializeField] private int _startingLevel;
        [SerializeField] private ZombieSpawnerLevel[] _zombieSpawnerLevels;

        public static ZombieSpawnerLevel CurrentZombieSpawnerLevel => _levels[_currentLevel - 1];

        private static bool _init;
        private static ZombieSpawnerLevel[] _levels;
        private static int _currentLevel;
        public static int CurrentLevel => _currentLevel;

        public static bool IncrementLevel()
        {
            if (_currentLevel + 1 > _levels.Length)
            {
                return false;
            }
            _currentLevel++;
            return true;
        }

        public static void ResetLevel() => _currentLevel = 1;

        private void Start()
        {
            if (_init) { Destroy(gameObject); }
            else
            {
                DontDestroyOnLoad(gameObject);
                if (_startingLevel < 1 || _startingLevel > _zombieSpawnerLevels.Length)
                {
                    _startingLevel = 1;
                }
                _currentLevel = _startingLevel;
                _levels = _zombieSpawnerLevels;
                _init = true;
            }
        }
    }
}
