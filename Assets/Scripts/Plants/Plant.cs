using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(HealthManager), typeof(BoxCollider2D))]
    public abstract class Plant : MonoBehaviour
    {
        public abstract PlantProperties PlantProps { get; }
    }
}
