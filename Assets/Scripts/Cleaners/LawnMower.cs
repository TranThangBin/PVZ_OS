using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class LawnMower : MonoBehaviour
    {
        [SerializeField] private float _velocity;
        [SerializeField] private float _damage;
        [SerializeField] private Rigidbody2D _rb;

        private void OnDestroy() => DOTween.Kill(this);

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out HealthManager healthManager))
            {
                healthManager.ReduceHealth(_damage);
                if (_rb.linearVelocity == Vector2.zero)
                {
                    DOTween.
                        Sequence(this).
                        AppendCallback(() => _rb.linearVelocity = _velocity * Vector2.right);
                }
            }
        }
    }
}