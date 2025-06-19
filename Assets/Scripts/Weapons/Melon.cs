using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Weapon))]
    public class Melon : MonoBehaviour, Weapon.IOnWeaponCollisionEnter
    {
        [SerializeField] private MelonProps _props;
        [SerializeField] private SpriteRenderer _melonSprite;
        [SerializeField] private ParticleSystem _ps;

        private void OnDestroy() => DOTween.Kill(this);

        public void Targeting(Rigidbody2D target)
        {
            float moveAmount = _props.FlyTime * target.linearVelocityX;
            Vector2 destination = target.transform.position + Vector3.right * moveAmount;

            transform.
                DOJump(destination, _props.JumpForce, 1, _props.FlyTime).
                Join(transform.DORotate(new(0, 0, -360), _props.FlyTime, RotateMode.WorldAxisAdd)).
                SetEase(Ease.Linear).
                OnComplete(() =>
                {
                    DOTween.Sequence(this).
                        AppendInterval(1).
                        AppendCallback(() => Destroy(gameObject));
                }).
                SetId(this);
        }

        public void OnWeaponCollisionEnter(Collision2D collision)
        {
            DOTween.Kill(this);
            collision.otherCollider.enabled = false;
            _melonSprite.enabled = false;
            _ps.Play();
        }
    }
}
