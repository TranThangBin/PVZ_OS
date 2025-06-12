using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Jalapeno : Plant, HealthManager.IInfiniteHealth
    {
        [SerializeField] private JalapenoProperties _jalapenoProps;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start()
        {
            DOTween.
                Sequence(this).
                AppendInterval(_jalapenoProps.DelayTime).
                AppendCallback(() =>
                {
                    Fire fire = Instantiate(_jalapenoProps.Fire, transform.parent);
                    fire.gameObject.layer = gameObject.layer;
                    Destroy(gameObject);
                });
        }

        public override PlantProperties PlantProps => _jalapenoProps.PlantProps;
    }
}
