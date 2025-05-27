using System.Linq;
using UnityEngine;

namespace Game
{
    public class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnTimer;
        [SerializeField] private GameObject _zombie;

        private Transform[] _spawnDestinations;

        private float _timer;

        public void Awake()
        {
            _timer = _spawnTimer;
            _spawnDestinations = GetComponentsInChildren<Transform>().Where(t => t != transform).ToArray();
        }

        public void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                int idx = Random.Range(0, _spawnDestinations.Length);
                Instantiate(_zombie, _spawnDestinations[idx].position, Quaternion.identity, _spawnDestinations[idx]);
                _timer = _spawnTimer;
            }
        }
    }
}
