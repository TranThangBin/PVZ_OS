using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BoxCollider2DPatch : MonoBehaviour
    {
        private float _initialYPos;

        private readonly UnityEvent<Collision2D> OnCollision2DPatch = new();

        private void Start()
        {
            _initialYPos = transform.position.y;
            foreach (IOnCollisionEnter2DPatch handler in GetComponents<IOnCollisionEnter2DPatch>())
            {
                OnCollision2DPatch.AddListener(handler.OnCollisionEnter2DPatch);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            float errorMargin = 3;
            if (collision.transform.position.y <= _initialYPos + errorMargin &&
                collision.transform.position.y >= _initialYPos - errorMargin)
            {
                OnCollision2DPatch.Invoke(collision);
            }
        }

        public interface IOnCollisionEnter2DPatch
        {
            void OnCollisionEnter2DPatch(Collision2D collision);
        }
    }
}
