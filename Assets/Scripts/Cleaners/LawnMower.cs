using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(BoxCollider2DPatch))]
    public class LawnMower : MonoBehaviour, BoxCollider2DPatch.IOnCollisionEnter2DPatch
    {
        [SerializeField] private float _velocity;
        [SerializeField] private int _damage;
        [SerializeField] private Rigidbody2D _rb;

        public void OnCollisionEnter2DPatch(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out HealthManager healthManager))
            {
                healthManager.ReduceHealth(_damage);
                if (_rb.linearVelocity == Vector2.zero) { _rb.linearVelocity = _velocity * Vector2.right; }
            }
        }
    }
}