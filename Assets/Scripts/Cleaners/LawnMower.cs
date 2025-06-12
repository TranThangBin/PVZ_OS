using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(PatchedBoxCollider2D))]
    public class LawnMower : MonoBehaviour, PatchedBoxCollider2D.IOnPatchedCollisionEnter2D
    {
        [SerializeField] private float _velocity;
        [SerializeField] private int _damage;
        [SerializeField] private Rigidbody2D _rb;

        public void PatchedOnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out HealthManager healthManager))
            {
                healthManager.ReduceHealth(_damage);
                if (_rb.linearVelocity == Vector2.zero) { _rb.linearVelocity = _velocity * Vector2.right; }
            }
        }
    }
}