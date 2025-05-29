using UnityEngine;

namespace Game
{
    public abstract class Plant : MonoBehaviour, ISelectable
    {
        [SerializeField] private int _plantCost;
        public bool ActionOnLocation(Transform location, SunManager sunManager)
        {
            if (!CanSelect(sunManager) || location.GetComponentInChildren<Plant>() != null)
            {
                return false;
            }
            Instantiate(gameObject, location.position, Quaternion.identity, location);
            sunManager.DecrementSunStore(_plantCost);
            return true;
        }

        public bool CanSelect(SunManager sunManager)
        {
            return sunManager.Buyable(_plantCost);
        }
    }
}
