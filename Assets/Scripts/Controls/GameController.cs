using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private MiscProperties _miscProps;
        [SerializeField] private TextMesh _sunDisplay;
        [SerializeField] private Transform _plantSelectorGrid;

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

        private void OnDestroy() => _invalidSelectAnimation.Kill();

        private void Awake()
        {
            Plant[] seeds = _miscProps.SeedBank;
            for (int i = 0; i < seeds.Length && i < _plantSelectorGrid.childCount; i++)
            {
                SeedPacket seedPacket = Instantiate(_miscProps.SeedPacket, _plantSelectorGrid.GetChild(i));
                seedPacket.SetPlant(seeds[i]);
                _onSunStoreChange.AddListener(seedPacket.OnSunStoreChange);
            }
        }

        private void Start()
        {
            SunStore = _miscProps.InitialSun;
            Color sunDisplayColor = _sunDisplay.color;
            _invalidSelectAnimation = DOTween.
                Sequence().
                AppendCallback(() => _sunDisplay.color = Color.red).AppendInterval(0.1f).
                AppendCallback(() => _sunDisplay.color = sunDisplayColor).AppendInterval(0.1f).
                SetAutoKill(false).
                SetLoops(2).
                Pause();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                HandleSelection(mousePosition);
                HandleCollectSun(mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                HandleLawnInteraction(mousePosition);
            }
        }

        private void HandleSelection(Vector2 mousePosition)
        {
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

        private void HandleLawnInteraction(Vector2 mousePosition)
        {
            if (_selected == null) { return; }

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 1, LayerMask.GetMask("LawnCell"));

            if (hit.collider != null)
            {
                _selected.ActionOnLawn(hit.collider.transform, (cost) =>
                {
                    SunStore -= cost;
                    Selected = null;
                });
            }
        }

        private void HandleCollectSun(Vector2 mousePosition)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 1, LayerMask.GetMask("Sun"));

            if (hit.collider != null)
            {
                Sun sun = hit.collider.GetComponent<Sun>();
                sun.
                    ToTheEnd().
                    PrependCallback(() => hit.collider.enabled = false).
                    Append(sun.transform.
                        DOMove(_sunDisplay.transform.position,
                            Utils.CalculateTime(sun.transform.position, _sunDisplay.transform.position, 82)).
                        OnComplete(() => SunStore += 25));
            }
        }
    }
}
