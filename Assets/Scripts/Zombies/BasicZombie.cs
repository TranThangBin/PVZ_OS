using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D), typeof(HealthManager), typeof(RangeCast))]
    public class BasicZombie : MonoBehaviour,
        HealthManager.IDestroyOnOutOfHealth, RangeCast.IOnRangeCastHit
    {
        [SerializeField] private BasicZombieProperties _basicZombieProps;

        private Rigidbody2D _rb;
        private Vector2 _direction = Vector2.left;

        private void OnDestroy() => DOTween.Kill(this);

        private void Awake() => _rb = GetComponent<Rigidbody2D>();

        private void Start() => _rb.linearVelocity = _basicZombieProps.MovementSpeed * _direction;

        public int Health => _basicZombieProps.Hp;

        public IEnumerable<RangeCast.RangeCastProperties> GetRangeCastProps()
        {
            yield return new(_direction, _basicZombieProps.AttackRange, Color.black);
        }
        public void OnRangeCastHit(RangeCast sender, Collider2D collider)
        {
            if (collider.TryGetComponent(out HealthManager health))
            {
                _rb.linearVelocity = Vector2.zero;
                DOTween.
                    Sequence(this).
                    AppendCallback(() =>
                    {
                        health.ReduceHealth(_basicZombieProps.Damage);
                        if (health == null)
                        {
                            _rb.linearVelocity = _basicZombieProps.MovementSpeed * _direction;
                            DOTween.Kill(this);
                        }
                    }).
                    AppendInterval(_basicZombieProps.AttackCooldown).
                    SetLoops(-1);
            }
        }
    }
}
