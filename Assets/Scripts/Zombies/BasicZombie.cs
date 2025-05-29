using UnityEngine;

namespace Game
{
    public class BasicZombie : MonoBehaviour
    {
        [SerializeField] private float _velocity;
        [SerializeField] private float _damage;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Timer _attackTimer;

        private HealthManager _plantHealth;

        private void Start()
        {
            _rb.linearVelocity = _velocity * Vector2.left;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            GameObject gameObject = collision.collider.gameObject;
            if (gameObject.layer == LayerMask.NameToLayer("Ally") && gameObject.TryGetComponent(out _plantHealth))
            {
                _rb.linearVelocity = Vector2.zero;
                _plantHealth.ReduceHealth(_damage);
                _attackTimer.TimerRestart();
            }
        }

        private void OnCollisionExit2D(Collision2D _)
        {
            if (_plantHealth == null)
            {
                _rb.linearVelocity = _velocity * Vector2.left;
                _attackTimer.TimerReset();
            }
        }

        public void OnTimerTimeOut()
        {
            if (_plantHealth != null)
            {
                _plantHealth.ReduceHealth(_damage);
                _attackTimer.TimerRestart();
            }

            if (_plantHealth == null || _plantHealth.IsOutOfHealth())
            {
                _rb.linearVelocity = _velocity * Vector2.left;
                _plantHealth = null;
                _attackTimer.TimerReset();
            }
        }
    }
}
