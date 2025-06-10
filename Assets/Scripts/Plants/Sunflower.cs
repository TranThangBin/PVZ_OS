using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Sunflower : Plant
    {
        [SerializeField] private Sun _sun;

        private SunflowerProperties SunflowerProps => PlantsProps.Sunflower;

        public override PlantProperties PlantProps => SunflowerProps.PlantProps;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start()
        {
            DOTween.
                Sequence(this).
                AppendInterval(SunflowerProps.SunGenerateTime).
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
