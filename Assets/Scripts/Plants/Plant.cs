using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public abstract class Plant : MonoBehaviour, ILawnAction, IValuable
    {
        [SerializeField] private int _plantCost;
        public void ActionOnLawn(Transform location, UnityAction<GameObject> onSuccess)
        {
            if (location.GetComponentInChildren<Plant>() != null)
            {
                return;
            }
            onSuccess.Invoke(Instantiate(gameObject, location));
        }

        public int GetValue()
        {
            return _plantCost;
        }
    }
}
