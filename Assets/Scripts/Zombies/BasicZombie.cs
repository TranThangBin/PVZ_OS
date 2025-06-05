using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public class BasicZombie : MonoBehaviour,
        HealthManager.IOnDamageTaken, HealthManager.IDestroyOnOutOfHealth
    {
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _velocity;
        [SerializeField] private float _damage;
        [SerializeField] private Rigidbody2D _rb;

        private Tween _attackTween;
        private HealthManager _plantHealth;

        private void Start()
        {
            _rb.linearVelocity = _velocity * Vector2.left;
            _attackTween = DOTween.
                Sequence().
                AppendCallback(() => Assert.IsNotNull(_plantHealth)).
                AppendCallback(() =>
                {
                    _plantHealth.ReduceHealth(_damage);
                    if (_plantHealth.IsOutOfHealth())
                    {
                        _rb.linearVelocity = _velocity * Vector2.left;
                        _attackTween.Pause();
                    }
                }).
                AppendInterval(_attackCooldown).
                SetLoops(-1).
                SetAutoKill(false).
                Pause();
        }

        private void OnDestroy()
        {
            _attackTween.Kill();
        }

        private void FixedUpdate()
        {
            if (!_attackTween.IsPlaying())
            {
                float rayDistance = 3.5f;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, rayDistance, LayerMask.GetMask("Ally"));
                Debug.DrawRay(transform.position, Vector2.left * rayDistance, Color.black);
                if (hit.collider != null)
                {
                    _rb.linearVelocity = Vector2.zero;
                    _plantHealth = hit.collider.GetComponent<HealthManager>();
                    _attackTween.Restart();
                }
            }
        }

        public void OnDamageTaken(HealthManager sender)
        {
            SpriteRenderer sr = sender.GetComponent<SpriteRenderer>();
            Assert.IsNotNull(sr);
            sender.BlinkOnDamageTaken(sr);
        }
    }
}
