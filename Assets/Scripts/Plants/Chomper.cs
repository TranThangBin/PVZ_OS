using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Plant), typeof(RangeCast))]
    public class Chomper : MonoBehaviour, Plant.IPlant, HealthManager.IDestroyOnOutOfHealth, RangeCast.IOnRangeCastHit
    {
        [SerializeField] private ChomperProperties _chomperProps;

        private void OnDestroy() => DOTween.Kill(this);

        public PlantProperties PlantProps => _chomperProps.PlantProps;

        public int Health => _chomperProps.Hp;

        public IEnumerable<RangeCast.RangeCastProperties> GetRangeCastProps()
        {
            yield return new(Vector2.right, _chomperProps.Range, LayerMask.GetMask("Enemy"), Color.red);
        }
        public void OnRangeCastHit(RangeCast sender, Collider2D collider)
        {
            if (!DOTween.IsTweening(this) && collider.TryGetComponent(out HealthManager enemyHealth))
            {
                enemyHealth.ReduceHealth(_chomperProps.Damage);
                DOTween.
                    Sequence(this).
                    AppendCallback(() => sender.enabled = false).
                    AppendInterval(_chomperProps.ChewingInterval).
                    AppendCallback(() => sender.enabled = true);
            }
        }
    }
}
