using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class SunSpawner : MonoBehaviour
    {
        [SerializeField] private SpawnersProperties _spawnerProps;
        [SerializeField] private Transform _sunSpawnStart;
        [SerializeField] private Transform _sunSpawnEnd;

        private SunSpanwerProperties SunSpawnerProps => _spawnerProps.SunSpanwer;

        private void Start()
        {
            DOTween.
                   Sequence(this).
                   AppendInterval(SunSpawnerProps.SpawnTime).
                   AppendCallback(() =>
                   {
                       Vector2 spawnPos = new Vector2(Random.Range(_sunSpawnStart.position.x, _sunSpawnEnd.position.x),
                            _sunSpawnStart.position.y) + SunSpawnerProps.Padding;

                       Sun sun = Instantiate(SunSpawnerProps.Sun, spawnPos, Quaternion.identity, transform);

                       float targetY = Random.Range(_sunSpawnStart.position.y, _sunSpawnEnd.position.y);
                       sun.
                           StartLifeTime().
                           Prepend(sun.transform.
                               DOMoveY(targetY,
                                   Utils.CalculateTime(sun.transform.position, new(spawnPos.x, targetY), 12)
                            ));
                   }).
                   SetLoops(-1);
        }

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }
    }
}
