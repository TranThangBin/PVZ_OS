using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class SunSpawner : MonoBehaviour
    {
        public Transform SunSpawnStart;
        public Transform SunSpawnEnd;
        public Sun Sun;
        public float SunSpawnInterval;

        private void Start() =>
            InvokeRepeating(nameof(SpawnSun), SunSpawnInterval, SunSpawnInterval);

        private void SpawnSun()
        {
            Vector2 spawnPos = new Vector2(Random.
                Range(SunSpawnStart.position.x, SunSpawnEnd.position.x),
                SunSpawnStart.position.y) +
                new Vector2(0, 30);

            Sun sun = Instantiate(Sun, spawnPos, Quaternion.identity, transform);

            float targetY = Random.Range(SunSpawnStart.position.y, SunSpawnEnd.position.y);
            sun.
                StartLifeTime().
                Prepend(sun.MoveTo(new(spawnPos.x, targetY)));
        }
    }
}
