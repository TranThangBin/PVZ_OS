using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Shovel : MonoBehaviour, ISelectable
    {
        [SerializeField] private SpriteRenderer _srSelectedOverlay;

        public void ActionOnLawn(Transform lawnCell, UnityAction<int> onSuccess)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D rc = Physics2D.Raycast(mousePos, Vector2.zero, 1, LayerMask.GetMask("Ally", "Protector"));
            if (rc.collider != null && rc.collider.TryGetComponent(out Plant plant))
            {
                Destroy(plant.gameObject);
            }
            onSuccess.Invoke(0);
        }

        public void SetSelected(bool isSelected) => _srSelectedOverlay.enabled = isSelected;
    }
}
