using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(BoxCollider2DPatch))]
    public abstract class PlantWeapon : MonoBehaviour, BoxCollider2DPatch.IOnCollisionEnter2DPatch
    {
        public abstract PlantWeaponProperties PlantWeaponProps { get; }

        public void OnCollisionEnter2DPatch(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out HealthManager healthManager))
            {
                healthManager.ReduceHealth(PlantWeaponProps.Damage);
                if (PlantWeaponProps.DestroyOnImpact) { Destroy(gameObject); }
            }
        }
    }
}
