using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Plant))]
    public class Sunflower : MonoBehaviour, Plant.IPlant, HealthManager.IDestroyOnOutOfHealth
    {
        [SerializeField] private SunflowerProperties _sunflowerProps;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start() =>
            DOTween.
                Sequence(this).
                AppendInterval(_sunflowerProps.SunGenerateInterval).
                AppendCallback(() =>
                {
                    Sun sun = Instantiate(_sunflowerProps.Sun, transform.parent);
                    sun.
                        StartLifeTime().
                        Prepend(sun.transform.DOJump(transform.position, 2, 1, 1));
                }).
                SetLoops(-1);

        public PlantProperties PlantProps => _sunflowerProps.PlantProps;

        public int Health => _sunflowerProps.Hp;
    }
}
