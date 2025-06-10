using DG.Tweening;

namespace Game
{
    public class Jalapeno : Plant
    {
        private JalapenoProperties JalapenoProps => PlantsProps.Jalapeno;

        public override PlantProperties PlantProps => JalapenoProps.PlantProps;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start()
        {
            DOTween.
                Sequence(this).
                AppendInterval(JalapenoProps.DelayTime).
                AppendCallback(() =>
                {
                    Instantiate(JalapenoProps.Fire, transform.parent);
                    Destroy(gameObject);
                });
        }
    }
}
