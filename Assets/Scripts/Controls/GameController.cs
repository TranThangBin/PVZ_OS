using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Transform _plantSelectorGrid;
        [SerializeField] private SeedPacket _seedPacket;
        [SerializeField] private Plant[] _plants;
        [SerializeField] private SunManager _sunManager;

        ILawnAction _selected;

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
                    IValuable valuable = hit.collider.gameObject.GetComponentInChildren<IValuable>();

                    if (valuable != null && _sunManager.Buyable(valuable) || valuable == null)
                    {
                        _selected = hit.collider.gameObject.GetComponentInChildren<ILawnAction>();
                    }
                }
            }
        }

        private void HandleLawnInteraction()
        {
            if (_selected != null && Input.GetMouseButtonUp(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 1, LayerMask.GetMask("LawnCell", "Sun"));

                if (hit.collider != null &&
                    hit.collider.gameObject.layer != LayerMask.NameToLayer("Sun"))
                {
                    _selected.ActionOnLawn(hit.collider.transform, (gameObj) =>
                    {
                        if (gameObj.TryGetComponent(out IValuable valuable))
                        {
                            _sunManager.DecrementSunStore(valuable);
                        }
                        _selected = null;
                    });
                }
            }
        }
    }
}
