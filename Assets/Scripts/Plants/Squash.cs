using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Squash : Plant
    {
        [SerializeField] private PlantDamages _plantDamages;
        [SerializeField] private PlantChargeTimes _plantChargeTimes;
        [SerializeField] private PlantRanges _plantRanges;
        [SerializeField] private float _jumpForce;

        private bool _kill = false;

        private void OnDestroy() => DOTween.Kill(this);

        private void FixedUpdate()
        {
            if (!DOTween.IsTweening(this))
            {
                float rayLength = _plantRanges.GetValue(PlantID);
                RaycastHit2D hit = RunTimeUtils.
                    Raycast(transform.position, Vector3.right, rayLength, LayerMask.GetMask("Enemy"), Color.red);

                if (hit.collider != null)
                {
                    transform.
                        DOJump(hit.collider.transform.position, _jumpForce, 1, 2).
                        SetEase(Ease.InBack).
                        AppendCallback(() => _kill = true).
                        PrependInterval(_plantChargeTimes.GetValue(PlantID)).
                        SetId(this);
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (_kill && collision.gameObject.TryGetComponent(out HealthManager zombieHealth))
            {
                zombieHealth.ReduceHealth(_plantDamages.GetValue(PlantID));
                Destroy(gameObject);
            }
        }
    }
}
