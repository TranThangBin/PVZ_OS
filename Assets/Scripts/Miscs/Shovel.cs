using UnityEngine;

namespace Game
{
    public class Shovel : MonoBehaviour, ISelectable
    {
        public bool CanSelect(SunManager _)
        {
            return true;
        }

        public bool ActionOnLocation(Transform location, SunManager sunManager)
        {
            Plant plant = location.GetComponentInChildren<Plant>();

            if (plant != null)
            {
                Destroy(plant.gameObject);
            }

            return true;
        }
    }
}
