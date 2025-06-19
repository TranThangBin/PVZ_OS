using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class ZombieSpawner : MonoBehaviour
    {
        public Transform ProgressBar;

        private ZombieSpawnerLevel _level;
        private int _difficulty;
        private int _wave;
        private float _progressPerWave;
        private float _progress;
        private float Progress
        {
            get => _progress;
            set
            {
                _progress = Mathf.Min(1, value);
                if (_progress >= 0.999) { _progress = 1; }
                ProgressBar.DOScaleX(_progress, _progress).SetId(this);
            }
        }

        private void OnDestroy() => DOTween.Kill(this);

        private void Start()
        {
            _level = PlayerLevels.CurrentZombieSpawnerLevel;
            _difficulty = _level.MinDifficulty;
            _progressPerWave = 1f / _level.Waves;
            InvokeRepeating(nameof(SpawnZombies),
                _level.ZombieSpawnInterval, _level.ZombieSpawnInterval);
        }

        private void SpawnZombies()
        {
            int zombieCount = _level.Difficulties.GetZombiesAmount(_difficulty);
            float progressPerZombie = _progressPerWave / zombieCount;

            for (int i = 0; i < zombieCount; i++)
            {
                int idx = Random.Range(0, transform.childCount);
                Transform zombieParent = transform.GetChild(idx);
                BasicZombie zombie = Instantiate(_level.BasicZombie, zombieParent);
                zombie.tag = zombieParent.tag;
                zombie.OnZombieDeath.AddListener(() =>
                {
                    Progress += progressPerZombie;
                    if (Progress == 1)
                    {
                        Trophy trophy = Instantiate(_level.Trophy, zombie.transform.position, Quaternion.identity);
                        trophy.transform.DOJump(trophy.transform.position, 10, 1, 1).SetId(this);
                    }
                });
            }
            _difficulty += _level.DifficultyIncrement;
            _wave++;
            if (_wave == _level.Waves) { CancelInvoke(); }
        }
    }
}
