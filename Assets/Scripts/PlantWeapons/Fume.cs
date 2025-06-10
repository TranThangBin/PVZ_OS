using UnityEngine;

namespace Game
{
    public class Fume : MonoBehaviour
    {
        [SerializeField] private GameProperties _gameProps;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out HealthManager healthManager))
            {
                healthManager.ReduceHealth(_gameProps.PlantWeapons.Fume.Damage);
            }
        }
    }
}
