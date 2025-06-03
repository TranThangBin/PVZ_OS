using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Shovel : MonoBehaviour, ILawnAction
    {
        public void ActionOnLawn(Transform lawnCell, UnityAction<GameObject> onSuccess)
        {
            Plant plant = lawnCell.GetComponentInChildren<Plant>();

            if (plant != null)
            {
                Destroy(plant.gameObject);
                onSuccess.Invoke(gameObject);
            }
        }
    }
}
