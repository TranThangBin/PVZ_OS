using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Sunflower : Plant
    {
        [SerializeField] private PlantChargeTimes _plantChargeTimes;
        [SerializeField] private Sun _sun;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start()
        {
            DOTween.
                Sequence(this).
                AppendInterval(_plantChargeTimes.GetValue(PlantID)).
                AppendCallback(() =>
                {
                    Sun sun = Instantiate(_sun, transform.parent);
                    sun.
                        StartLifeTime().
                        Prepend(sun.transform.DOJump(transform.position, 2, 1, 1));
                }).
                SetLoops(-1);
        }
    }
}
