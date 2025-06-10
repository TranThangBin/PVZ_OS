using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Chomper : Plant
    {
        private ChomperProperties ChomperProps => PlantsProps.Chomper;
        private Tween _cooldownTween;
        private bool _ready;

        public override PlantProperties PlantProps => ChomperProps.PlantProps;

        private void OnDestroy() => _cooldownTween.Kill();

        private void Start()
        {
            _cooldownTween = DOTween.
                Sequence().
                AppendCallback(() => _ready = false).
                AppendInterval(ChomperProps.ChewTime).
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
                    Vector2.right, ChomperProps.VisionLength, LayerMask.GetMask("Enemy"), Color.red);

                if (rc.collider != null && rc.collider.TryGetComponent(out HealthManager zombieHealth))
                {
                    zombieHealth.ReduceHealth(ChomperProps.Damage);
                    _cooldownTween.Restart();
                }
            }
        }
    }
}
