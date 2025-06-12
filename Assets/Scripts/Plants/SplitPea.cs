using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(RangeCast))]
    public class SplitPea : Plant, RangeCast.IOnRangeCastHit, HealthManager.IDestroyOnOutOfHealth
    {
        [SerializeField] private SplitPeaProperties _splitPeaProps;

        private void OnDestroy() => DOTween.Kill(this);

        public override PlantProperties PlantProps => _splitPeaProps.PlantProps;

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
                    AppendCallback(() => sender.enabled = false).
                    AppendCallback(() => InstantiatePea().Targeting(transform.position + Vector3.right * _splitPeaProps.Range)).
                    AppendInterval(0.4f).
                    AppendCallback(() => InstantiatePea().Targeting(transform.position + Vector3.left * _splitPeaProps.Range)).
                    AppendInterval(0.2f).
                    AppendCallback(() => InstantiatePea().Targeting(transform.position + Vector3.left * _splitPeaProps.Range)).
                    AppendInterval(_splitPeaProps.ShootingInterval).
                    AppendCallback(() => sender.enabled = true);
            }
        }

        public int Health => _splitPeaProps.Hp;

        private Pea InstantiatePea()
        {
            Pea pea = Instantiate(_splitPeaProps.Pea, transform.parent);
            pea.gameObject.layer = gameObject.layer;
            return pea;
        }
    }
}
