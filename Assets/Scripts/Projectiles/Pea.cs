using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public class Pea : MonoBehaviour, IProjectile
    {
        [SerializeField] private float _damage;
        [SerializeField] private int _flyVelocity;

        private Rigidbody2D _rb;

        public void Fire(Vector2 direction)
        {
            _rb.linearVelocity = _flyVelocity * direction;
        }

        public void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            Assert.IsNotNull(_rb, $"{typeof(Pea)} require a {typeof(Rigidbody2D)}");
            Assert.IsTrue(_rb.bodyType == RigidbodyType2D.Kinematic, $"{typeof(Pea)} body should be kinematic");
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out HealthManager healthManager))
            {
                Destroy(gameObject);
                healthManager.ReduceHealth(_damage);
            }
        }
    }
}
