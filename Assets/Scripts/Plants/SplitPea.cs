using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class SplitPea : Plant
    {
        [SerializeField] private float _attackCooldown;
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
                AppendCallback(() => Attack(Vector2.right)).
                AppendInterval(0.5f).
                AppendCallback(() => Attack(Vector2.left)).
                AppendInterval(0.25f).
                AppendCallback(() =>
                {
                    Attack(Vector2.left);
                    _cooldownTween.Restart();
                }).
                SetAutoKill(false).
                Pause();
        }

        private void FixedUpdate()
        {
            if (_ready)
            {
                float rayDistance = 200;
                RaycastHit2D rc = Physics2D.Raycast(transform.position + Vector3.left * rayDistance / 2, Vector2.right, rayDistance, LayerMask.GetMask("Enemy"));
                Debug.DrawRay(transform.position + Vector3.left * rayDistance / 2, Vector3.right * rayDistance, Color.red);
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

        private void Attack(Vector3 direction)
        {
            GameObject gameObject = Instantiate(_projectile, transform.position + direction / 2, Quaternion.identity, transform.parent);
            IProjectile projectile = gameObject.GetComponent<IProjectile>();
            projectile.Fire(direction);
        }
    }
}
