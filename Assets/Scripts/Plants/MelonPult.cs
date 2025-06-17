using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(RangeCast), typeof(Plant))]
    public class MelonPult : MonoBehaviour, RangeCast.IOnRangeCastHit
    {
        [SerializeField] private MelonPultProps _melonPultProps;
        [SerializeField] private Transform _pult;
        [SerializeField] private SpriteRenderer _melonProjectile;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start() => DOTween.Sequence(this).AppendInterval(1);

        public IEnumerable<RangeCast.RangeCastProperties> GetRangeCastProps()
        {
            yield return new(Vector2.right, _melonPultProps.Range, Color.red);
        }
        public void OnRangeCastHit(RangeCast sender, Collider2D collider)
        {
            if (!DOTween.IsTweening(this))
            {
                float pultTime = 0.5f;
                Tween pultTween = DOTween.
                    Sequence(this).
                    Append(_pult.DOLocalRotate(new(0, 0, -80), pultTime)).
                    Join(_pult.DOLocalMove(new(0, 5.5f), pultTime)).
                    SetEase(Ease.InCubic).
                    SetLoops(2, LoopType.Yoyo).
                    Pause();

                DOTween.
                    Sequence(this).
                    AppendCallback(() => pultTween.Play()).
                    AppendInterval(pultTime).
                    AppendCallback(() =>
                    {
                        sender.enabled = false;
                        _melonProjectile.enabled = false;

                        Melon melon = Instantiate(_melonPultProps.Melon, _melonProjectile.transform.position,
                            _melonProjectile.transform.rotation, transform.parent);
                        melon.gameObject.layer = gameObject.layer;
                        melon.tag = tag;
                        melon.Targeting(collider.attachedRigidbody);
                    }).
                    AppendInterval(_melonPultProps.ShootingInterval).
                    AppendCallback(() =>
                    {
                        sender.enabled = true;
                        _melonProjectile.enabled = true;
                    });
            }
        }
    }
}
