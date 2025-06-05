using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Squash : Plant
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _jumpForce;
        [SerializeField] private Collider2D _collider;

        private bool _kill = false;

        private void FixedUpdate()
        {
            if (!DOTween.IsTweening(this))
            {
                float rayDistance = 20;
                float rayOffset = rayDistance / 2;
                RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.left * rayOffset, Vector3.right, rayDistance, LayerMask.GetMask("Enemy"));
                Debug.DrawRay(transform.position + Vector3.left * rayOffset, Vector3.right * rayDistance, Color.red);
                if (hit.collider != null)
                {
                    transform.
                        DOJump(hit.collider.transform.position, _jumpForce, 1, 2).
                        SetEase(Ease.InBack).
                        AppendCallback(() => _kill = true).
                        SetId(this);
                }
            }
        }

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (_kill && collision.gameObject.TryGetComponent(out HealthManager zombieHealth))
            {
                zombieHealth.ReduceHealth(_damage);
                Destroy(gameObject);
            }
        }
    }
}
