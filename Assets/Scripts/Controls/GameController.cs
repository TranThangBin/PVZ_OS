using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private int _initialSun;
        [SerializeField] private TextMesh _sunDisplay;
        [SerializeField] private Transform _plantSelectorGrid;
        [SerializeField] private SeedPacket _seedPacket;
        [SerializeField] private Plant[] _plants;

        private Tween _invalidSelectAnimation;
        private readonly UnityEvent<int> _onSunStoreChange = new();
        private int _sunStore;
        private int SunStore
        {
            get => _sunStore;
            set
            {
                _sunStore = Mathf.Max(0, value);
                _sunDisplay.text = value.ToString();
                _onSunStoreChange.Invoke(_sunStore);
            }
        }

        private ISelectable _selected;
        private ISelectable Selected
        {
            get => _selected;
            set
            {
                if (value == null || _selected == value)
                {
                    _selected?.SetSelected(false);
                    _selected = null;
                }
                else
                {
                    value.SetSelected(true);
                    _selected?.SetSelected(false);
                    _selected = value;
                }
            }
        }

        private void Awake()
        {
            for (int i = 0; i < _plants.Length && i < _plantSelectorGrid.transform.childCount; i++)
            {
                SeedPacket seedPacket = Instantiate(_seedPacket, _plantSelectorGrid.transform.GetChild(i));
                seedPacket.SetPlant(_plants[i]);
                _onSunStoreChange.AddListener(seedPacket.OnSunStoreChange);
            }
        }

        private void Start()
        {
            SunStore = _initialSun;
            Color sunDisplayColor = _sunDisplay.color;
            _invalidSelectAnimation = DOTween.
                Sequence().
                AppendCallback(() => _sunDisplay.color = Color.red).AppendInterval(0.1f).
                AppendCallback(() => _sunDisplay.color = sunDisplayColor).AppendInterval(0.1f).
                SetAutoKill(false).
                SetLoops(2).
                Pause();
        }

        private void OnDestroy()
        {
            _invalidSelectAnimation.Kill();
        }

        private void Update()
        {
            HandleSelection();
            HandleLawnInteraction();
            HandleCollectSun();
        }

        private void HandleSelection()
        {
            if (!Input.GetMouseButtonDown(0)) { return; }

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 1, LayerMask.GetMask("Selectable"));

            if (hit.collider == null) { return; }

            IAvailable available = hit.collider.GetComponentInChildren<IAvailable>();

            if (available == null || available.IsAvailable())
            {
                Selected = hit.collider.GetComponentInChildren<ISelectable>();
            }
            else
            {
                _invalidSelectAnimation.Restart();
            }
        }

        private void HandleLawnInteraction()
        {
            if (_selected == null || !Input.GetMouseButtonUp(0)) { return; }

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 1, LayerMask.GetMask("LawnCell"));

            if (hit.collider != null)
            {
                _selected.ActionOnLawn(hit.collider.transform, (gameObj, cost) =>
                {
                    SunStore -= cost;
                    Selected = null;
                });
            }
        }

        private void HandleCollectSun()
        {
            if (!Input.GetMouseButtonDown(0)) { return; }

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 1, LayerMask.GetMask("Sun"));

            if (hit.collider != null)
            {
                Sun sun = hit.collider.GetComponent<Sun>();
                sun.
                    ToTheEnd().
                    PrependCallback(() => hit.collider.enabled = false).
                    Append(sun.transform.
                        DOMove(_sunDisplay.transform.position, sun.CalculateTime(_sunDisplay.transform.position, 6)).
                        OnComplete(() => SunStore += 25));
            }
        }
    }
}
