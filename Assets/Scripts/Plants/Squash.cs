using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Squash : Plant
    {
        [SerializeField] private float _jumpForce;

        private SquashProperties SquashProps => PlantsProps.Squash;
        private bool _kill = false;

        public override PlantProperties PlantProps => SquashProps.PlantProps;

        private void OnDestroy() => DOTween.Kill(this);

        private void FixedUpdate()
        {
            if (!DOTween.IsTweening(this))
            {
                float rayLength = SquashProps.VisionLength;
                RaycastHit2D hit = Utils.
                    Raycast(transform.position + Vector3.left * rayLength, Vector3.right, rayLength * 2, LayerMask.GetMask("Enemy"), Color.red);

                if (hit.collider != null)
                {
                    transform.
                        DOJump(hit.collider.transform.position, _jumpForce, 1, 2).
                        SetEase(Ease.InBack).
                        AppendCallback(() => _kill = true).
                        PrependInterval(SquashProps.DelayTime).
                        SetId(this);
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (_kill && collision.collider.TryGetComponent(out HealthManager zombieHealth))
            {
                zombieHealth.ReduceHealth(SquashProps.Damage);
                Destroy(gameObject);
            }
        }
    }
}
