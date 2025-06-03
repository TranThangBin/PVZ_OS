using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Sun : MonoBehaviour
    {
        [SerializeField] private float _velocity;
        [SerializeField] private Timer _timer;
        [SerializeField] private Rigidbody2D _rb;

        public UnityEvent OnSunDestroy = new();

        private Vector3 _targetPosition;
        private float _velocityMultiplier = 1;

        private void Awake()
        {
            _targetPosition = transform.position;
        }

        private void Update()
        {
            if (transform.position != _targetPosition)
            {
                if (!_timer.TimerIsStopped())
                {
                    _timer.TimerStop();
                }

                transform.position = Vector2.MoveTowards(transform.position, _targetPosition, GetActualVelocity(Time.deltaTime));
            }
            else if (_timer.TimerIsStopped())
            {
                _timer.TimerStart();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("SunStore"))
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            OnSunDestroy.Invoke();
        }

        public void SetTargetPosition(Vector2 position)
        {
            _targetPosition = position;
        }

        private float GetActualVelocity(float delta)
        {
            return _velocity * _velocityMultiplier * delta;
        }

        public void SetVelocityMultiplier(float multiplier)
        {
            _velocityMultiplier = multiplier;
        }
    }
}
