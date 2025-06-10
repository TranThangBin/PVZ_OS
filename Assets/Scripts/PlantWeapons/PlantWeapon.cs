using UnityEngine;

namespace Game
{
    public abstract class PlantWeapon : MonoBehaviour
    {
        [SerializeField] private PlantWeaponsProperties _plantWeaponsProps;

        protected PlantWeaponsProperties PlantWeaponsProps => _plantWeaponsProps;
        protected abstract PlantWeaponProperties PlantWeaponProps { get; }

        private float _initialYPos;

        private void Start() => _initialYPos = transform.position.y;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            float errorMargin = 3;
            if (collision.transform.position.y <= _initialYPos + errorMargin &&
                collision.transform.position.y >= _initialYPos - errorMargin &&
                collision.collider.TryGetComponent(out HealthManager healthManager))
            {
                healthManager.ReduceHealth(PlantWeaponProps.Damage);
                if (PlantWeaponProps.DestroyOnCollision)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
