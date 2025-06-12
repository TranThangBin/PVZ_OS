using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class SunSpawner : MonoBehaviour
    {
        [SerializeField] private MiscProperties _miscProps;
        [SerializeField] private Transform _sunSpawnStart;
        [SerializeField] private Transform _sunSpawnEnd;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start()
        {
            DOTween.
                   Sequence(this).
                   AppendInterval(_miscProps.SunSpawnInterval).
                   AppendCallback(() =>
                   {
                       Vector2 spawnPos = new Vector2(Random.Range(_sunSpawnStart.position.x, _sunSpawnEnd.position.x),
                            _sunSpawnStart.position.y) + new Vector2(0, 20);

                       Sun sun = Instantiate(_miscProps.Sun, spawnPos, Quaternion.identity, transform);

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
    }
}
