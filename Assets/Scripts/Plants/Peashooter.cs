using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(RangeCast), typeof(Plant))]
    public class Peashooter : MonoBehaviour, RangeCast.IOnRangeCastHit
    {
        [SerializeField] private PeashooterProps _peashooterProps;
        [SerializeField] private Transform _shootPosition;

        private void OnDestroy() => DOTween.Kill(this);

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
                        Pea pea = Instantiate(_peashooterProps.Pea,
                            _shootPosition.position, Quaternion.identity, transform.parent);
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
