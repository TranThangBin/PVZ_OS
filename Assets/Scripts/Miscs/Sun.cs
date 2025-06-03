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
        private bool _goToEnd = false;

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

                transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _velocity * Time.deltaTime);
            }
            else if (_timer.TimerIsStopped())
            {
                _timer.TimerStart();
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (_goToEnd && collision.collider.gameObject.layer == LayerMask.NameToLayer("SunStore"))
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

        public void SetEndPoint(Vector2 position)
        {
            _velocity *= 6;
            _targetPosition = position;
            _goToEnd = true;
        }
    }
}
