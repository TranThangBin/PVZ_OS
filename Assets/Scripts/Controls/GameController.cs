using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Transform _plantSelectorGrid;
        [SerializeField] private SeedPacket _seedPacket;
        [SerializeField] private Plant[] _plants;
        [SerializeField] private SunManager _sunManager;

        ISelectable _selected;

        private void Awake()
        {
            for (int i = 0; i < _plants.Length && i < _plantSelectorGrid.transform.childCount; i++)
            {
                SeedPacket seedPacket = Instantiate(_seedPacket, _plantSelectorGrid.transform.GetChild(i));
                seedPacket.SetPlant(_plants[i]);
            }
        }

        private void Update()
        {
            HandleSelection();
            HandleLawnInteraction();
        }

        private void HandleSelection()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 1, LayerMask.GetMask("Selectable"));

                if (hit.collider != null)
                {
                    ISelectable selectable = hit.collider.GetComponentInChildren<ISelectable>();
                    if (selectable.CanSelect(_sunManager))
                    {
                        _selected = selectable;
                    }
                }
            }
        }

        private void HandleLawnInteraction()
        {
            if (Input.GetMouseButtonUp(0) && _selected != null)
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 1, LayerMask.GetMask("LawnCell", "Sun"));

                if (hit.collider != null &&
                    hit.collider.gameObject.layer != LayerMask.NameToLayer("Sun") &&
                    _selected.ActionOnLocation(hit.collider.transform, _sunManager)
                    )
                {
                    _selected = null;
                }
            }
        }
    }
}
