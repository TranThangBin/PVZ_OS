using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Sun : MonoBehaviour
    {
        [SerializeField] private float _velocity;
        [SerializeField] private Timer _timer;
        [SerializeField] private Rigidbody2D _rb;

        private bool _goToEnd = false;
        private Tween _activeTween;

        private void Start()
        {
            _timer.TimerStart();
        }

        public void SetTargetPosition(Vector2 position)
        {
            if (_goToEnd) { return; }
            _timer.TimerReset();

            _activeTween?.Kill();
            _activeTween = transform.DOMove(position, CalculateTime(position, _velocity)).
                OnComplete(() => _timer.TimerStart());
        }

        public void SetEndPoint(Vector2 position, UnityAction callback)
        {
            if (_goToEnd) { return; }

            _goToEnd = true;
            _timer.TimerStop();

            _activeTween?.Kill();
            _activeTween = transform.DOMove(position, CalculateTime(position, _velocity * 6)).OnComplete(() =>
            {
                callback();
                Destroy(gameObject);
            });
        }

        private float CalculateTime(Vector2 position, float velocity)
        {
            float edge1 = Mathf.Abs(transform.position.y - position.y);
            float edge2 = Mathf.Abs(transform.position.x - position.x);
            float distance = Mathf.Sqrt(edge1 * edge1 + edge2 * edge2);
            return distance / velocity;
        }
    }
}
