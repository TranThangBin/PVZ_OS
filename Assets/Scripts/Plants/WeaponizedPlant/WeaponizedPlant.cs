using DG.Tweening;
using UnityEngine;

namespace Game
{
    public abstract class WeaponizedPlant : Plant
    {
        private Tween _cooldownTween;
        private bool _ready;

        public override PlantProperties PlantProps => WeaponizedPlantProps.PlantProps;
        protected abstract WeaponizedPlantProperties WeaponizedPlantProps { get; }

        private void Start()
        {
            _cooldownTween = DOTween.
                Sequence(this).
                AppendInterval(WeaponizedPlantProps.AttackRechargeTime).
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
                        Append(Attack(WeaponizedPlantProps.Weapon.gameObject, rc.rigidbody)).
                        AppendCallback(() => _cooldownTween.Restart());
                    _ready = false;
                }
            }
        }

        private void OnDestroy() => DOTween.Kill(this);

        protected abstract Tween Attack(GameObject projectile, Rigidbody2D target);

        protected virtual RaycastHit2D Raycast()
        {
            return Utils.Raycast(
                transform.position,
                Vector2.right, WeaponizedPlantProps.VisionLength, LayerMask.GetMask("Enemy"), Color.red);
        }
    }
}
