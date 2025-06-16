using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(RangeCast), typeof(Plant))]
    public class MelonPult : MonoBehaviour, RangeCast.IOnRangeCastHit
    {
        [SerializeField] private MelonPultProps _melonPultProps;

        private void OnDestroy() => DOTween.Kill(this);

        public IEnumerable<RangeCast.RangeCastProperties> GetRangeCastProps()
        {
            yield return new(Vector2.right, _melonPultProps.Range, Color.red);
        }
        public void OnRangeCastHit(RangeCast sender, Collider2D collider)
        {
            if (!DOTween.IsTweening(this))
            {
                DOTween.
                    Sequence(this).
                    AppendCallback(() =>
                    {
                        sender.enabled = false;
                        Melon melon = Instantiate(_melonPultProps.Melon, transform.parent);
                        melon.gameObject.layer = gameObject.layer;
                        melon.tag = tag;
                        melon.Targeting(collider.attachedRigidbody);
                    }).
                    AppendInterval(_melonPultProps.ShootingInterval).
                    AppendCallback(() => sender.enabled = true);
            }
        }
    }
}
