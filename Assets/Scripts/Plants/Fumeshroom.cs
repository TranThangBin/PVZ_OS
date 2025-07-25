using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Plant), typeof(RangeCast))]
    public class Fumeshroom : MonoBehaviour, RangeCast.IOnRangeCastHit
    {
        [SerializeField] private FumeshroomProps _props;
        [SerializeField] private Transform _shootPosition;

        private void OnDestroy() => DOTween.Kill(this);

        public IEnumerable<RangeCast.RangeCastProperties> GetRangeCastProps()
        {
            yield return new RangeCast.RangeCastProperties(Vector2.right, _props.Range, Color.red);
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
                        Weapon fume = Instantiate(_props.Fume,
                            _shootPosition.position, Quaternion.identity, transform.parent);
                        fume.tag = tag;
                        fume.gameObject.layer = gameObject.layer;
                    }).
                    AppendInterval(_props.ShootingInterval).
                    AppendCallback(() => sender.enabled = true);
            }
        }
    }
}
