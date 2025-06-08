using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Jalapeno : Plant
    {
        [SerializeField] private PlantChargeTimes _plantChargeTimes;
        [SerializeField] private GameObject _fire;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start()
        {
            DOTween.
                Sequence(this).
                AppendInterval(_plantChargeTimes.GetValue(PlantID)).
                AppendCallback(() =>
                {
                    Instantiate(_fire, transform.parent);
                    Destroy(gameObject);
                });
        }
    }
}
