using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(HealthManager))]
    public class Plant : MonoBehaviour, HealthManager.IDestroyOnOutOfHealth, HealthManager.IHealthy
    {
        [field: SerializeField] public PlantProps Props { get; private set; }
        public int Health => Props.Hp;
    }
}
