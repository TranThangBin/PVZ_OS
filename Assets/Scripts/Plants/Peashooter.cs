using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(RangeCast), typeof(Plant))]
    public class Peashooter : MonoBehaviour, RangeCast.IOnRangeCastHit
    {
        [SerializeField] private PeashooterProps _peashooterProps;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start() => DOTween.Sequence(this).AppendInterval(1);

        public IEnumerable<RangeCast.RangeCastProperties> GetRangeCastProps()
        {
            yield return new(Vector2.right, _peashooterProps.Range, Color.red);
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
                        Pea pea = Instantiate(_peashooterProps.Pea, transform.parent);
                        pea.gameObject.layer = gameObject.layer;
                        pea.tag = tag;
                        pea.Targeting(Vector2.right);
                    }).
                    AppendInterval(_peashooterProps.ShootingInterval).
                    AppendCallback(() => sender.enabled = true);
            }
        }
    }
}
