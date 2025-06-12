using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class PatchedBoxCollider2D : MonoBehaviour
    {
        private float _initialYPos;

        private readonly UnityEvent<Collision2D> PvZOnCollisionEnter2D = new();

        private void Start()
        {
            _initialYPos = transform.position.y;
            foreach (IOnPatchedCollisionEnter2D handler in GetComponents<IOnPatchedCollisionEnter2D>())
            {
                PvZOnCollisionEnter2D.AddListener(handler.PatchedOnCollisionEnter2D);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            float errorMargin = 3;
            if (collision.transform.position.y <= _initialYPos + errorMargin &&
                collision.transform.position.y >= _initialYPos - errorMargin)
            {
                PvZOnCollisionEnter2D.Invoke(collision);
            }
        }

        public interface IOnPatchedCollisionEnter2D
        {
            void PatchedOnCollisionEnter2D(Collision2D collision);
        }
    }
}
