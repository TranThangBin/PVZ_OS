using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(RangeCast))]
    public class Peashooter : Plant, RangeCast.IOnRangeCastHit, HealthManager.IDestroyOnOutOfHealth
    {
        [SerializeField] private PeashooterProperties _peashooterProps;

        private void OnDestroy() => DOTween.Kill(this);

        public override PlantProperties PlantProps => _peashooterProps.PlantProps;

        public IEnumerable<RangeCast.RangeCastProperties> GetRangeCastProps()
        {
            yield return new(Vector2.right, _peashooterProps.Range, Color.red);
        }
        public void OnRangeCastHit(RangeCast sender, Collider2D collider)
        {
            if (!DOTween.IsTweening(this))
            {
                Pea pea = Instantiate(_peashooterProps.Pea, transform.parent);
                pea.Targeting(transform.position + Vector3.right * _peashooterProps.Range);
                pea.gameObject.layer = gameObject.layer;

                DOTween.
                    Sequence(this).
                    AppendCallback(() => sender.enabled = false).
                    AppendInterval(_peashooterProps.ShootingInterval).
                    AppendCallback(() => sender.enabled = true);
            }
        }

        public int Health => _peashooterProps.Hp;
    }
}
