using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private int _initialSun;
        [SerializeField] private TextMesh _sunDisplay;
        [SerializeField] private Transform _plantSelectorGrid;
        [SerializeField] private SeedPacket _seedPacket;
        [SerializeField] private Plant[] _plants;

        private int _sunStore;
        private int SunStore
        {
            get => _sunStore; set
            {
                _sunStore = Mathf.Max(0, value);
                _sunDisplay.text = value.ToString();
            }
        }

        ILawnAction _selected;

        private void Awake()
        {
            for (int i = 0; i < _plants.Length && i < _plantSelectorGrid.transform.childCount; i++)
            {
                SeedPacket seedPacket = Instantiate(_seedPacket, _plantSelectorGrid.transform.GetChild(i));
                seedPacket.SetPlant(_plants[i]);
            }
        }

        private void Start()
        {
            SunStore = _initialSun;
        }

        private void Update()
        {
            HandleSelection();
            HandleLawnInteraction();
            HandleCollectSun();
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

                    if (valuable != null && SunStore >= valuable.GetValue() || valuable == null)
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
                            SunStore -= valuable.GetValue();
                        }
                        _selected = null;
                    });
                }
            }
        }

        private void HandleCollectSun()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 1, LayerMask.GetMask("Sun"));

                if (hit.collider != null)
                {
                    Sun sun = hit.collider.GetComponent<Sun>();
                    sun.SetTargetPosition(_sunDisplay.transform.position);
                    sun.SetVelocityMultiplier(5);

                    sun.OnSunDestroy.RemoveListener(OnSunDestroyListener);
                    sun.OnSunDestroy.AddListener(OnSunDestroyListener);
                }
            }
        }

        private void OnSunDestroyListener()
        {
            SunStore += 25;
        }
    }
}
