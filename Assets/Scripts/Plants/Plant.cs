using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(HealthManager), typeof(BoxCollider2D))]
    public class Plant : MonoBehaviour
    {
        public void Planting(Transform location, UnityAction<GameObject> onSuccess)
        {
            if (location.GetComponentInChildren<Plant>() == null)
            {
                onSuccess.Invoke(Instantiate(gameObject, location));
            }
        }

        public interface IPlant
        {
            PlantProperties PlantProps { get; }
        }
    }
}
