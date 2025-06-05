using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Shovel : MonoBehaviour, ISelectable
    {
        [SerializeField] private SpriteRenderer _srSelectedOverlay;

        public void ActionOnLawn(Transform lawnCell, UnityAction<GameObject, int> onSuccess)
        {
            Plant plant = lawnCell.GetComponentInChildren<Plant>();

            if (plant != null)
            {
                Destroy(plant.gameObject);
                onSuccess.Invoke(gameObject, 0);
            }
        }

        public void SetSelected(bool isSelected)
        {
            _srSelectedOverlay.enabled = isSelected;
        }
    }
}
