using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] private SpawnersProperties _spawnerProps;
        [SerializeField] private Transform _spawnYLocationStart;
        [SerializeField] private Transform _spawnYLocationEnd;

        private ZombieSpanwerProperties ZombieSpawnerProps => _spawnerProps.ZombieSpawner;
        private Vector2[] _spawnLocations;

        private void Awake()
        {
            _spawnLocations = new Vector2[ZombieSpawnerProps.RowCount];
            float distance = _spawnYLocationStart.position.y - _spawnYLocationEnd.position.y;


            float height = distance / ZombieSpawnerProps.RowCount;
            for (int i = 0; i < _spawnLocations.Length; i++)
            {
                _spawnLocations[i] = new(transform.position.x, _spawnYLocationStart.position.y - height * (i + 1) + height / 2);
            }
        }

        private void Start()
        {
            DOTween.
                Sequence(this).
                AppendInterval(ZombieSpawnerProps.SpawnTime).
                AppendCallback(() =>
                {
                    int idx = Random.Range(0, _spawnLocations.Length);
                    Instantiate(ZombieSpawnerProps.BasicZombie, _spawnLocations[idx], Quaternion.identity, transform);
                }).
                SetLoops(-1);
        }

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }
    }
}
