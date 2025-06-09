using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Chomper : Plant
    {
        [SerializeField] private PlantChargeTimes _plantChargeTimes;
        [SerializeField] private PlantDamages _plantDamages;
        [SerializeField] private PlantRanges _plantRanges;

        private Tween _cooldownTween;
        private bool _ready;

        private void OnDestroy() => _cooldownTween.Kill();

        private void Start()
        {
            _cooldownTween = DOTween.
                Sequence().
                AppendCallback(() => _ready = false).
                AppendInterval(_plantChargeTimes.GetValue(PlantID)).
                AppendCallback(() => _ready = true).
                SetAutoKill(false).
                Pause();

            _ready = true;
        }

        private void FixedUpdate()
        {
            if (_ready)
            {
                RaycastHit2D rc = Utils.Raycast(
                    transform.position,
                    Vector2.right, _plantRanges.GetValue(PlantID).Range, LayerMask.GetMask("Enemy"), Color.red);

                if (rc.collider != null && rc.collider.TryGetComponent(out HealthManager zombieHealth))
                {
                    zombieHealth.ReduceHealth(_plantDamages.GetValue(PlantID));
                    _cooldownTween.Restart();
                }
            }
        }
    }
}
