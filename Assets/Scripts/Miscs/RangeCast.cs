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
                RaycastHit2D rc = Utils.Raycast(transform.position, props.Direction, props.Distance, props.LayerMask, props.RayColor);
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
            public readonly LayerMask LayerMask;
            public readonly Color RayColor;

            public RangeCastProperties(Vector2 direction, float distance, LayerMask layerMask, Color rayColor)
            {
                Direction = direction;
                Distance = distance;
                LayerMask = layerMask;
                RayColor = rayColor;
            }
        }
    }
}
