using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(HealthManager), typeof(RangeCast))]
    public class BasicZombie : MonoBehaviour,
        HealthManager.IDestroyOnOutOfHealth, HealthManager.IOnDamageTaken,
        RangeCast.IOnRangeCastHit
    {
        [SerializeField] private BasicZombieProps _basicZombieProps;
        [SerializeField] private Animator _anim;
        [SerializeField] private ParticleSystem _ps;

        private Rigidbody2D _rb;
        private Vector2 _direction = Vector2.left;

        private void OnDestroy() => DOTween.Kill(this);

        private void Awake() => _rb = GetComponent<Rigidbody2D>();

        private void Start() => _rb.linearVelocity = _basicZombieProps.MovementSpeed * _direction;

        private void SetEatingAnimation(bool value)
        {
            if (_anim != null)
            {
                _anim.SetBool("IsEating", value);
            }
        }

        public int Health => _basicZombieProps.Hp;
        public void OnDamageTaken(HealthManager sender)
        {
            GetComponent<SpriteRenderer>().
                DOColor(new(1, 0.25f, 0.25f), 0.1f).
                SetLoops(2, LoopType.Yoyo).
                SetId(this);
        }

        public IEnumerable<RangeCast.RangeCastProperties> GetRangeCastProps()
        {
            yield return new(_direction, _basicZombieProps.AttackRange, Color.black);
        }
        public void OnRangeCastHit(RangeCast sender, Collider2D collider)
        {
            if (!DOTween.IsTweening(this) && collider.TryGetComponent(out HealthManager health) && CompareTag(health.tag))
            {

                DOTween.
                    Sequence(this).
                    AppendCallback(() =>
                    {
                        sender.enabled = false;
                        health.ReduceHealth(_basicZombieProps.Damage);
                        SetEatingAnimation(true);
                        _rb.linearVelocity = Vector2.zero;
                        _ps.Play();
                        if (health.IsOutOfHealth())
                        {
                            _rb.linearVelocity = _basicZombieProps.MovementSpeed * _direction;
                            SetEatingAnimation(false);
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
