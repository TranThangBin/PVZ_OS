using UnityEngine;

namespace Game
{
    public class Fire : MonoBehaviour
    {
        [SerializeField] private PlantWeaponDamages _plantWeaponDamages;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out HealthManager healthManager))
            {
                healthManager.ReduceHealth(_plantWeaponDamages.GetValue(PlantWeaponID.Fire));
            }
        }
    }
}
