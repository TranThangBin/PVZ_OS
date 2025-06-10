using UnityEngine;

namespace Game
{
    public class Fire : MonoBehaviour
    {
        [SerializeField] private PlantWeaponsProperties _plantWeaponProps;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out HealthManager healthManager))
            {
                healthManager.ReduceHealth(_plantWeaponProps.Fire.PlantWeaponProps.Damage);
            }
        }
    }
}
