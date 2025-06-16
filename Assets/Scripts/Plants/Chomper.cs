using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(RangeCast), typeof(Plant))]
    public class Chomper : MonoBehaviour, RangeCast.IOnRangeCastHit
    {
        [SerializeField] private ChomperProps _chomperProps;
        [SerializeField] private Animator _anim;

        private void OnDestroy() => DOTween.Kill(this);

        public IEnumerable<RangeCast.RangeCastProperties> GetRangeCastProps()
        {
            yield return new(Vector2.right, _chomperProps.Range, Color.red);
        }
        public void OnRangeCastHit(RangeCast sender, Collider2D collider)
        {
            if (!DOTween.IsTweening(this) && collider.TryGetComponent(out HealthManager enemyHealth))
            {
                DOTween.
                    Sequence(this).
                    AppendCallback(() =>
                    {
                        enemyHealth.ReduceHealth(_chomperProps.Damage);
                        sender.enabled = false;
                        _anim.SetBool("IsChewing", true);
                    }).
                    AppendInterval(_chomperProps.ChewingInterval).
                    AppendCallback(() =>
                    {
                        sender.enabled = true;
                        _anim.SetBool("IsChewing", false);
                    });
            }
        }
    }
}
