using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(RangeCast), typeof(Plant))]
    public class SplitPea : MonoBehaviour, RangeCast.IOnRangeCastHit
    {
        [SerializeField] private SplitPeaProps _splitPeaProps;
        [SerializeField] private Transform _frontShootPosition;
        [SerializeField] private Transform _backShootPosition;

        private void OnDestroy() => DOTween.Kill(this);

        public IEnumerable<RangeCast.RangeCastProperties> GetRangeCastProps()
        {
            yield return new(Vector2.right, _splitPeaProps.Range, Color.red);
            yield return new(Vector2.left, _splitPeaProps.Range, Color.red);
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
                        InstantiatePea(_frontShootPosition).Targeting(Vector2.right);
                    }).
                    AppendInterval(0.4f).
                    AppendCallback(() => InstantiatePea(_backShootPosition).Targeting(Vector2.left)).
                    AppendInterval(0.2f).
                    AppendCallback(() => InstantiatePea(_backShootPosition).Targeting(Vector2.left)).
                    AppendInterval(_splitPeaProps.ShootingInterval).
                    AppendCallback(() => sender.enabled = true);
            }
        }

        private Pea InstantiatePea(Transform t)
        {
            Pea pea = Instantiate(_splitPeaProps.Pea, t.position,
                Quaternion.identity, transform.parent);
            pea.gameObject.layer = gameObject.layer;
            pea.tag = tag;
            return pea;
        }
    }
}
