using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Peashooter : Plant
    {
        [SerializeField] float _attackCooldown;
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
                AppendCallback(() =>
                {
                    GameObject gameObject = Instantiate(_projectile, transform.position + (Vector3.right / 2), Quaternion.identity, transform.parent);
                    IProjectile projectile = gameObject.GetComponent<IProjectile>();
                    projectile.Fire(Vector2.right);
                    _ready = false;
                    _cooldownTween.Restart();
                }).
                SetAutoKill(false).
                Pause();
        }

        private void FixedUpdate()
        {
            if (_ready)
            {
                float rayDistance = 100;
                RaycastHit2D rc = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, LayerMask.GetMask("Enemy"));
                Debug.DrawRay(transform.position, Vector3.right * rayDistance, Color.red);
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
    }
}
