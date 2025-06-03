using UnityEngine;

namespace Game
{
    public class SunSpawner : MonoBehaviour
    {
        [SerializeField] private Sun _sun;
        [SerializeField] private Transform _sunSpawnStart;
        [SerializeField] private Transform _sunSpawnEnd;

        public void OnTimerTimeOut(Timer sender)
        {
            int padY = 10;
            Vector3 spawnPos = new Vector2(Random.Range(_sunSpawnStart.position.x, _sunSpawnEnd.position.x),
                _sunSpawnStart.position.y + padY);

            Sun sun = Instantiate(_sun, spawnPos, Quaternion.identity, transform);
            sun.SetTargetPosition(new Vector2(sun.transform.position.x, Random.Range(_sunSpawnStart.position.y, _sunSpawnEnd.position.y)));

            sender.TimerRestart();
        }

    }
}
