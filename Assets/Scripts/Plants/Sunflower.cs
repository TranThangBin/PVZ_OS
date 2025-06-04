using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Sunflower : Plant
    {
        [SerializeField] private float _sunGenerateTime;
        [SerializeField] private Sun _sun;

        private void Start()
        {
            DOTween.
                Sequence(this).
                AppendInterval(_sunGenerateTime).
                AppendCallback(() =>
                {
                    Sun sun = Instantiate(_sun, transform.parent);
                    sun.
                        StartLifeTime().
                        Prepend(sun.transform.DOJump(transform.position + Vector3.up * 5, 2, 1, 1)).
                        Play();
                }).
                SetLoops(-1);
        }

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }
    }
}
