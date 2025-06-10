using UnityEngine;

namespace Game
{
    public class Pea : MonoBehaviour
    {
        [SerializeField] private GameProperties _gameProps;
        [SerializeField] private Rigidbody2D _rb;

        private PeaProperties PeaProps => _gameProps.PlantWeapons.Pea;

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
