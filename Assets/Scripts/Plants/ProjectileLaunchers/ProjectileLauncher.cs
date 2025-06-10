using DG.Tweening;
using UnityEngine;

namespace Game
{
    public abstract class ProjectileLauncher : Plant
    {
        private Tween _cooldownTween;
        private bool _ready;

        public override PlantProperties PlantProps => ProjectileLauncherProps.PlantProps;
        protected abstract ProjectileLauncherProperties ProjectileLauncherProps { get; }

        private void Start()
        {
            _cooldownTween = DOTween.
                Sequence(this).
                AppendCallback(() => _ready = false).
                AppendInterval(ProjectileLauncherProps.AttackRechargeTime).
                AppendCallback(() => _ready = true).
                SetAutoKill(false).
                Pause();

            DOTween.
                Sequence(this).
                AppendInterval(1).
                AppendCallback(() => _ready = true);
        }

        private void FixedUpdate()
        {
            if (_ready)
            {
                RaycastHit2D rc = Raycast();

                if (rc.collider != null)
                {
                    DOTween.
                        Sequence(this).
                        Append(Attack(ProjectileLauncherProps.Projectile, rc.rigidbody)).
                        AppendCallback(() => _cooldownTween.Restart());
                }
            }
        }

        private void OnDestroy() => DOTween.Kill(this);

        protected abstract Tween Attack(GameObject projectile, Rigidbody2D target);

        protected virtual RaycastHit2D Raycast()
        {
            return Utils.Raycast(
                transform.position,
                Vector2.right, ProjectileLauncherProps.VisionLength, LayerMask.GetMask("Enemy"), Color.red);
        }
    }
}
