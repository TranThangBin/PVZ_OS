using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class RangeCast : MonoBehaviour
    {
        private IEnumerable<RangeCastProperties> _rangeCastProps;
        private readonly UnityEvent<RangeCast, Collider2D> OnRangeCastHit = new();

        private void Start()
        {
            if (TryGetComponent(out IRange range)) { _rangeCastProps = range.GetRangeCastProps(); }

            foreach (IOnRangeCastHit handler in GetComponents<IOnRangeCastHit>())
            {
                OnRangeCastHit.AddListener(handler.OnRangeCastHit);
            }
        }

        private void FixedUpdate()
        {
            foreach (RangeCastProperties props in _rangeCastProps)
            {
                Debug.DrawRay(transform.position, props.Direction * props.Distance, props.RayColor);
                RaycastHit2D rc = Physics2D.Raycast(transform.position, props.Direction, props.Distance, Physics2D.GetLayerCollisionMask(gameObject.layer));
                if (rc.collider != null)
                {
                    OnRangeCastHit.Invoke(this, rc.collider);
                }
            }
        }

        public interface IRange
        {
            IEnumerable<RangeCastProperties> GetRangeCastProps();
        }

        public interface IOnRangeCastHit : IRange
        {
            void OnRangeCastHit(RangeCast sender, Collider2D collider);
        }

        public readonly struct RangeCastProperties
        {
            public readonly Vector2 Direction;
            public readonly float Distance;
            public readonly Color RayColor;

            public RangeCastProperties(Vector2 direction, float distance, Color rayColor)
            {
                Direction = direction;
                Distance = distance;
                RayColor = rayColor;
            }
        }
    }
}
