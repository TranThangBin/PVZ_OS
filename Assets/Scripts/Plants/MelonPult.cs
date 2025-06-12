using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(RangeCast))]
    public class MelonPult : Plant, RangeCast.IOnRangeCastHit, HealthManager.IDestroyOnOutOfHealth
    {
        [SerializeField] private MelonPultProperties _melonPultProps;

        private void OnDestroy() => DOTween.Kill(this);

        public override PlantProperties PlantProps => _melonPultProps.PlantProps;

        public IEnumerable<RangeCast.RangeCastProperties> GetRangeCastProps()
        {
            yield return new(Vector2.right, _melonPultProps.Range, Color.red);
        }
        public void OnRangeCastHit(RangeCast sender, Collider2D collider)
        {
            if (!DOTween.IsTweening(this))
            {
                Melon melon = Instantiate(_melonPultProps.Melon, transform.parent);
                melon.gameObject.layer = gameObject.layer;
                melon.Targeting(collider.attachedRigidbody);

                DOTween.
                    Sequence(this).
                    AppendCallback(() => sender.enabled = false).
                    AppendInterval(_melonPultProps.ShootingInterval).
                    AppendCallback(() => sender.enabled = true);
            }
        }

        public int Health => _melonPultProps.Hp;
    }
}
