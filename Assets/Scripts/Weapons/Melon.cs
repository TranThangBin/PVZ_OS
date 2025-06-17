using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Weapon))]
    public class Melon : MonoBehaviour, Weapon.IOnWeaponCollision
    {
        [SerializeField] private MelonProps _props;
            
        private void OnDestroy() => DOTween.Kill(this);

        public void Targeting(Rigidbody2D target)
        {
            float moveAmount = _props.FlyTime * target.linearVelocityX;
            Vector2 destination = target.transform.position + Vector3.right * moveAmount;

            transform.
                DOJump(destination, _props.JumpForce, 1, _props.FlyTime).
                Join(transform.DORotate(new(0, 0, -90), _props.FlyTime)).
                SetEase(Ease.Linear).
                OnComplete(() =>
                {
                    DOTween.Sequence(this).
                        AppendInterval(1).
                        AppendCallback(() => Destroy(gameObject));
                }).
                SetId(this);
        }

        public void OnWeaponCollision(Collision2D collision)
        {
            Destroy(gameObject);
        }
    }
}
