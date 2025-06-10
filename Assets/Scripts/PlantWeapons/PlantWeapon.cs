using UnityEngine;

namespace Game
{
    public class PlantWeapon : MonoBehaviour
    {
        [SerializeField] private PlantWeapons _plantWeaponID;
        [SerializeField] private PlantWeaponsProperties _plantWeaponsProps;

        public PlantWeaponsProperties PlantWeaponsProps => _plantWeaponsProps;

        private PlantWeaponProperties PlantWeaponProps =>
            _plantWeaponsProps.GetWeaponProperties(_plantWeaponID);

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
