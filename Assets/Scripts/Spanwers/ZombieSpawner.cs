using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] private MiscProperties _miscProps;
        [SerializeField] private Transform _spawnYLocationStart;
        [SerializeField] private Transform _spawnYLocationEnd;

        private Vector2[] _spawnLocations;

        private void OnDestroy() => DOTween.Kill(this);

        private void Awake()
        {
            _spawnLocations = new Vector2[_miscProps.ZombieSpawnRows];
            float distance = _spawnYLocationStart.position.y - _spawnYLocationEnd.position.y;


            float height = distance / _spawnLocations.Length;
            for (int i = 0; i < _spawnLocations.Length; i++)
            {
                _spawnLocations[i] = new(transform.position.x, _spawnYLocationStart.position.y - height * (i + 1) + height / 2);
            }
        }

        private void Start()
        {
            DOTween.
                Sequence(this).
                AppendInterval(_miscProps.ZombieSpawnInterval).
                AppendCallback(() =>
                {
                    int idx = Random.Range(0, _spawnLocations.Length);
                    Instantiate(_miscProps.BasicZombie, _spawnLocations[idx], Quaternion.identity, transform);
                }).
                SetLoops(-1);
        }
    }
}
