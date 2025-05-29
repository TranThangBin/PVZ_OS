using UnityEngine;

namespace Game
{
    public class LawnMower : MonoBehaviour
    {
        [SerializeField] private float _velocity;
        [SerializeField] private float _damage;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Timer _lifetimeTimer;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out HealthManager healthManager))
            {
                healthManager.ReduceHealth(_damage);
                if (_rb.linearVelocity == Vector2.zero)
                {
                    _rb.linearVelocity = _velocity * Vector2.right;
                    _lifetimeTimer.TimerStart();
                }
            }
        }
    }
}