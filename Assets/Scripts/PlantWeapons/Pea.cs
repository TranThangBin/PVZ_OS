using UnityEngine;

namespace Game
{
    public class Pea : MonoBehaviour
    {
        [SerializeField] private PlantWeaponVelocities _plantWeaponVelocities;
        [SerializeField] private PlantWeaponDamages _plantWeaponDamages;
        [SerializeField] private Rigidbody2D _rb;

        public void Targeting(Vector2 direction) => _rb.linearVelocity = _plantWeaponVelocities.GetValue(PlantWeaponID.Pea) * direction;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out HealthManager healthManager))
            {
                Destroy(gameObject);
                healthManager.ReduceHealth(_plantWeaponDamages.GetValue(PlantWeaponID.Pea));
            }
        }
    }
}
