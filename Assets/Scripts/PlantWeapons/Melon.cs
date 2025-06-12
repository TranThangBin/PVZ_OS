using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(PlantWeapon))]
    public class Melon : MonoBehaviour, PlantWeapon.IPlantWeapon
    {
        [SerializeField] private MelonProperties _melonProps;

        private void OnDestroy() => DOTween.Kill(this);

        public void Targeting(Rigidbody2D target)
        {
            float moveAmount = _melonProps.FlyTime * target.linearVelocityX;
            Vector2 destination = target.position + Vector2.left * moveAmount;
            transform.
                DOJump(target.position, _melonProps.JumpForce, 1, _melonProps.FlyTime).
                Join(transform.DORotate(Vector3.back * 90, _melonProps.FlyTime)).
                SetEase(Ease.Linear).
                OnComplete(() => Destroy(gameObject)).
                SetId(this);
        }

        public PlantWeaponProperties PlantWeaponProps => _melonProps.PlantWeaponProps;
    }
}
