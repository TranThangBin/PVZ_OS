using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class SunSpawner : MonoBehaviour
    {
        [SerializeField] private float _sunDropTime;
        [SerializeField] private float _padY;
        [SerializeField] private Sun _sun;
        [SerializeField] private Transform _sunSpawnStart;
        [SerializeField] private Transform _sunSpawnEnd;

        private void Start()
        {
            DOTween.
                   Sequence(this).
                   AppendInterval(_sunDropTime).
                   AppendCallback(() =>
                   {
                       Vector3 spawnPos = new(Random.Range(_sunSpawnStart.position.x, _sunSpawnEnd.position.x),
                            _sunSpawnStart.position.y + _padY);

                       Sun sun = Instantiate(_sun, spawnPos, Quaternion.identity, transform);

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
