using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(PatchedBoxCollider2D))]
    public class PlantWeapon : MonoBehaviour, PatchedBoxCollider2D.IOnPatchedCollisionEnter2D
    {
        public void PatchedOnCollisionEnter2D(Collision2D collision)
        {
            PlantWeaponProperties props = GetComponent<IPlantWeapon>().PlantWeaponProps;
            if (collision.collider.TryGetComponent(out HealthManager healthManager))
            {
                healthManager.ReduceHealth(props.Damage);
                if (props.DestroyOnImpact) { Destroy(gameObject); }
            }
        }

        public interface IPlantWeapon
        {
            PlantWeaponProperties PlantWeaponProps { get; }
        }
    }
}
