using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Plant), typeof(RangeCast))]
    public class Squash : MonoBehaviour, Plant.IPlant, RangeCast.IOnRangeCastHit
    {
        [SerializeField] private SquashProperties _squashProps;

        private bool _kill = false;

        private void OnDestroy() => DOTween.Kill(this);

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (_kill && collision.collider.TryGetComponent(out HealthManager zombieHealth))
            {
                zombieHealth.ReduceHealth(_squashProps.Damage);
                Destroy(gameObject);
            }
        }

        public PlantProperties PlantProps => _squashProps.PlantProps;

        public IEnumerable<RangeCast.RangeCastProperties> GetRangeCastProps()
        {
            yield return new(Vector2.right, _squashProps.Range, LayerMask.GetMask("Enemy"), Color.red);
            yield return new(Vector2.left, _squashProps.Range, LayerMask.GetMask("Enemy"), Color.red);
        }
        public void OnRangeCastHit(RangeCast sender, Collider2D collider)
        {
            if (!DOTween.IsTweening(this))
            {
                sender.enabled = false;
                float moveAmount = _squashProps.JumpTime * collider.attachedRigidbody.linearVelocityX;
                Vector2 destination = collider.transform.position + Vector3.left * moveAmount;

                transform.
                    DOJump(destination, _squashProps.JumpForce, 1, _squashProps.JumpTime).
                    SetEase(Ease.Linear).
                    AppendCallback(() => _kill = true).
                    AppendInterval(1).
                    AppendCallback(() => Destroy(gameObject)).
                    SetId(this);
            }
        }
    }
}
