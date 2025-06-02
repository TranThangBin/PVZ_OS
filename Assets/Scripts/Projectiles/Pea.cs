using UnityEngine;

namespace Game
{
    public class Pea : MonoBehaviour, IProjectile
    {
        [SerializeField] private float _damage;
        [SerializeField] private int _flyVelocity;
        [SerializeField] private Rigidbody2D _rb;


        public void Fire(Vector2 direction)
        {
            _rb.linearVelocity = _flyVelocity * direction;
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
