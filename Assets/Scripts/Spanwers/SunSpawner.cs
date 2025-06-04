using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class SunSpawner : MonoBehaviour
    {
        [SerializeField] private Sun _sun;
        [SerializeField] private Transform _sunSpawnStart;
        [SerializeField] private Transform _sunSpawnEnd;
        [SerializeField] private float _padY;

        public void OnTimerTimeOut(Timer sender)
        {
            Vector3 spawnPos = new(Random.Range(_sunSpawnStart.position.x, _sunSpawnEnd.position.x),
                _sunSpawnStart.position.y + _padY);

            Sun sun = Instantiate(_sun, spawnPos, Quaternion.identity, transform);

            float targetY = Random.Range(_sunSpawnStart.position.y, _sunSpawnEnd.position.y);

            sun.
                StartLifeTime().
                Prepend(sun.transform.
                    DOMoveY(targetY,
                        sun.CalculateTime(new(spawnPos.x, targetY), 1)
                    ));

            sender.TimerRestart();
        }

    }
}
