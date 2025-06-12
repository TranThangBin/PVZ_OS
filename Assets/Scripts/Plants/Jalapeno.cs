using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Plant))]
    public class Jalapeno : MonoBehaviour, Plant.IPlant
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
                    Instantiate(_jalapenoProps.Fire, transform.parent);
                    Destroy(gameObject);
                });
        }

        public PlantProperties PlantProps => _jalapenoProps.PlantProps;
    }
}
