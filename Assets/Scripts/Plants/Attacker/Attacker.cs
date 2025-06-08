using DG.Tweening;
using UnityEngine;

namespace Game
{
    public abstract class Attacker : Plant
    {
        [SerializeField] private PlantChargeTimes _plantChargeTimes;
        [SerializeField] PlantRanges _plantRanges;
        [SerializeField] private GameObject _projectile;

        private bool _ready = false;
        private Tween _attackTween;
        private Tween _cooldownTween;

        private void Start()
        {
            _cooldownTween = DOTween.
                Sequence().
                AppendInterval(_plantChargeTimes.GetValue(PlantID)).
                AppendCallback(() => _ready = true).
                SetAutoKill(false);

            _attackTween = DOTween.
                Sequence().
                AppendCallback(() => _ready = false).
                Append(Attack(_projectile)).
                AppendCallback(() => _cooldownTween.Restart()).
                SetAutoKill(false).
                Pause();
        }

        private void FixedUpdate()
        {
            if (_ready)
            {
                float rayOffset = 0;
                float rayLength = _plantRanges.GetValue(PlantID).Range;
                if (_plantRanges.GetValue(PlantID).BothDirection)
                {
                    rayOffset = rayLength;
                    rayLength *= 2;
                }

                RaycastHit2D rc = RunTimeUtils.Raycast(
                    transform.position + Vector3.left * rayOffset,
                    Vector2.right, rayLength, LayerMask.GetMask("Enemy"), Color.red);

                if (rc.collider != null)
                {
                    _attackTween.Restart();
                }
            }
        }

        private void OnDestroy()
        {
            _attackTween.Kill();
            _cooldownTween.Kill();
        }

        protected abstract Tween Attack(GameObject projectile);
    }
}
