using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Pea : MonoBehaviour
    {
        [SerializeField] private GameProperties _gameProps;

        private PeaProperties PeaProps => _gameProps.PlantWeapons.Pea;

        private Rigidbody2D _rb;

        private void Awake() => _rb = GetComponent<Rigidbody2D>();

        public void Targeting(Vector2 direction) => _rb.linearVelocity = PeaProps.FlySpeed * direction;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out HealthManager healthManager))
            {
                healthManager.ReduceHealth(PeaProps.Damage);
                Destroy(gameObject);
            }
        }
    }
}
