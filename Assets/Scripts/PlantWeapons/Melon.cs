using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Melon : PlantWeapon
    {
        [SerializeField] private MelonProperties _melonProps;

        private void OnDestroy() => DOTween.Kill(this);

        public void Targeting(Rigidbody2D target)
        {
            float moveAmount = _melonProps.FlyTime * target.linearVelocityX;
            Vector2 destination = target.position + Vector2.right * moveAmount;

            transform.
                DOJump(destination, _melonProps.JumpForce, 1, _melonProps.FlyTime).
                Join(transform.DORotate(Vector3.back * 90, _melonProps.FlyTime)).
                SetEase(Ease.InOutQuart).
                OnComplete(() => Destroy(gameObject)).
                SetId(this);
        }

        public override PlantWeaponProperties PlantWeaponProps => _melonProps.PlantWeaponProps;
    }
}
