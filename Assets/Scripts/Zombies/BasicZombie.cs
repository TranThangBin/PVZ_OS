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
        [SerializeField] private Animator _anim;
        [SerializeField] private ParticleSystem _ps;

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
            if (!DOTween.IsTweening(this) && collider.TryGetComponent(out HealthManager health))
            {
                _rb.linearVelocity = Vector2.zero;
                _anim.SetBool("IsEating", true);
                sender.enabled = false;
                DOTween.
                    Sequence(this).
                    AppendCallback(() =>
                    {
                        health.ReduceHealth(_basicZombieProps.Damage);
                        _ps.Play();
                        if (health.IsOutOfHealth())
                        {
                            _rb.linearVelocity = _basicZombieProps.MovementSpeed * _direction;
                            _anim.SetBool("IsEating", false);
                            sender.enabled = true;
                            DOTween.Kill(this);
                        }
                    }).
                    AppendInterval(_basicZombieProps.AttackCooldown).
                    SetLoops(-1);
            }
        }
    }
}
