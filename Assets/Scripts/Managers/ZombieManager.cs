using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] private int _rowCount;
        [SerializeField] private GameObject _zombie;
        [SerializeField] private Transform _spawnYLocationStart;
        [SerializeField] private Transform _spawnYLocationEnd;

        private Vector2[] _spawnLocations;

        private void Awake()
        {
            _spawnLocations = new Vector2[_rowCount];
            float distance = _spawnYLocationStart.position.y - _spawnYLocationEnd.position.y;

            Assert.IsTrue(distance > 0);

            float height = distance / _rowCount;
            for (int i = 0; i < _spawnLocations.Length; i++)
            {
                _spawnLocations[i] = new Vector2(transform.position.x, _spawnYLocationStart.position.y - height * (i + 1) + height / 2);
            }
        }

        public void OnTimerTimeOut(Timer sender)
        {
            int idx = Random.Range(0, _spawnLocations.Length);
            Instantiate(_zombie, _spawnLocations[idx], Quaternion.identity, transform);
            sender.TimerRestart();
        }
    }
}
