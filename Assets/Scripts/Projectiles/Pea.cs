using UnityEngine;

namespace Game
{
    public class Pea : MonoBehaviour, IProjectile
    {
        [SerializeField] private ProjectileID _projectileID;
        [SerializeField] private ProjectileVelocities _projectileVelocities;
        [SerializeField] private ProjectileDamages _projectileDamages;
        [SerializeField] private Rigidbody2D _rb;

        public void Fire(Vector2 direction) => _rb.linearVelocity = _projectileVelocities.GetValue(_projectileID) * direction;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out HealthManager healthManager))
            {
                Destroy(gameObject);
                healthManager.ReduceHealth(_projectileDamages.GetValue(_projectileID));
            }
        }
    }
}
