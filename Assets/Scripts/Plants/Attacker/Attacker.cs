using DG.Tweening;
using UnityEngine;

namespace Game
{
    public abstract class Attacker : Plant
    {
        [SerializeField] float _attackCooldown;
        [SerializeField] float _rayLength;
        [SerializeField] float _rayOffset;
        [SerializeField] private GameObject _projectile;

        private bool _ready = false;
        private Tween _attackTween;
        private Tween _cooldownTween;

        private void Start()
        {
            _cooldownTween = DOTween.
                Sequence().
                AppendInterval(_attackCooldown).
                AppendCallback(() => _ready = true).
                SetAutoKill(false);

            _attackTween = DOTween.
                Sequence().
                AppendCallback(() => _ready = false).
                Append(Attack(_projectile)).
                AppendCallback(() => _cooldownTween.Restart()).
                SetAutoKill(false).
                Pause();
        }

        private void FixedUpdate()
        {
            if (_ready)
            {
                RaycastHit2D rc = Physics2D.Raycast(transform.position + Vector3.right * _rayOffset, Vector2.right, _rayLength, LayerMask.GetMask("Enemy"));
                Debug.DrawRay(transform.position + Vector3.right * _rayOffset, Vector3.right * _rayLength, Color.red);
                if (rc.collider != null)
                {
                    _attackTween.Restart();
                }
            }
        }

        private void OnDestroy()
        {
            _attackTween.Kill();
            _cooldownTween.Kill();
        }

        protected abstract Tween Attack(GameObject projectile);
    }
}
