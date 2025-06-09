using DG.Tweening;
using UnityEngine;

namespace Game
{
    public abstract class ProjectileLauncher : Plant
    {
        [SerializeField] private PlantChargeTimes _plantChargeTimes;
        [SerializeField] private PlantRanges _plantRanges;
        [SerializeField] private GameObject _projectile;

        private Tween _cooldownTween;
        private bool _ready;

        private void Start()
        {
            _cooldownTween = DOTween.
                Sequence(this).
                AppendInterval(_plantChargeTimes.GetValue(PlantID)).
                AppendCallback(() => _ready = true).
                SetAutoKill(false);
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

                RaycastHit2D rc = Utils.Raycast(
                    transform.position + Vector3.left * rayOffset,
                    Vector2.right, rayLength, LayerMask.GetMask("Enemy"), Color.red);

                if (rc.collider != null)
                {
                    DOTween.
                        Sequence(this).
                        Append(Attack(_projectile, rc.rigidbody)).
                        AppendCallback(() =>
                        {
                            _ready = false;
                            _cooldownTween.Restart();
                        });
                }
            }
        }

        private void OnDestroy() => DOTween.Kill(this);

        protected abstract Tween Attack(GameObject projectile, Rigidbody2D target);
    }
}
