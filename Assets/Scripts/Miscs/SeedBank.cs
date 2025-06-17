using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class SeedBank : MonoBehaviour
    {
        [SerializeField] private SeedBankProps _props;
        [SerializeField] private TextMeshPro _sunDisplay;
        [SerializeField] private Transform _seedsContainer;
        [SerializeField] private Transform _seedContainerStart;
        [SerializeField] private Transform _seedContainerEnd;

        private Tween _invalidSelectAnimation;
        private bool _mouseHitSun;
        private readonly UnityEvent<int> OnSunStoreChange = new();

        private int _sunStore;
        private int SunStore
        {
            get => _sunStore;
            set
            {
                _sunStore = Mathf.Max(0, value);
                _sunDisplay.text = value.ToString();
                OnSunStoreChange.Invoke(_sunStore);
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

        public void Start()
        {
            Plant[] seeds = _props.Seeds;
            for (int i = 0; i < seeds.Length && i < _props.MaxSeed; i++)
            {
                SeedPacket seedPacket = Instantiate(_props.SeedPacket, _seedsContainer.GetChild(i));
                seedPacket.SetPlant(seeds[i]);

                OnSunStoreChange.AddListener(seedPacket.OnSunStoreChange);
            }
            _invalidSelectAnimation = _sunDisplay.
                DOColor(Color.red, 0.1f).
                SetAutoKill(false).
                SetLoops(2, LoopType.Yoyo).
                Pause();
            SunStore = _props.InitialSun;
            MeshRenderer sunRenderer = _sunDisplay.GetComponent<MeshRenderer>();
            sunRenderer.sortingLayerName = "SeedBank";
        }

        private void Update()
        {
            bool lMouseDown = Input.GetMouseButtonDown(0);
            bool lMouseUp = Input.GetMouseButtonUp(0);

            if (!lMouseDown && !lMouseUp) { return; }

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero, 1,
                LayerMask.GetMask("Selectable", "Sun", "LawnCell"));

            Collider2D selectable = null;
            Collider2D sun = null;
            Collider2D lawnCell = null;

            foreach (RaycastHit2D hit in hits)
            {
                Collider2D collider = hit.collider;
                int hitLayer = collider.gameObject.layer;

                if (hitLayer == LayerMask.NameToLayer("Selectable"))
                {
                    selectable = collider;
                }
                else if (hitLayer == LayerMask.NameToLayer("Sun"))
                {
                    sun = collider;
                }
                else if (hitLayer == LayerMask.NameToLayer("LawnCell"))
                {
                    lawnCell = collider;
                }
            }

            if (lMouseDown)
            {
                if (sun != null)
                {
                    HandleCollectSun(sun);
                    _mouseHitSun = true;
                }
                else if (selectable != null)
                {
                    HandleSelection(selectable);
                }
            }

            if (lMouseUp)
            {
                if (lawnCell != null && !_mouseHitSun)
                {
                    HandleLawnInteraction(lawnCell);
                }
                _mouseHitSun = false;
            }
        }

        private void HandleSelection(Collider2D selection)
        {
            IAvailable available = selection.GetComponentInChildren<IAvailable>();
            if (available == null || available.IsAvailable())
            {
                ISelectable selectable = selection.GetComponentInChildren<ISelectable>();
                if (selectable != null) { Selected = selectable; }
            }
            else
            {
                _invalidSelectAnimation.Restart();
            }
        }

        private void HandleCollectSun(Collider2D sunCollider)
        {
            Sun sun = sunCollider.GetComponent<Sun>();
            DOTween.Kill(sun);
            sun.
                ToTheEnd(_sunDisplay.transform.position).
                OnStart(() => sunCollider.enabled = false).
                OnKill(() => SunStore += 25);
        }

        private void HandleLawnInteraction(Collider2D lawnCell)
        {
            if (_selected == null) { return; }

            _selected.ActionOnLawn(lawnCell.transform, (cost) =>
            {
                SunStore -= cost;
                Selected = null;
            });
        }

        [ContextMenu("Generate Colliders")]
        private void GenerateColliders()
        {
            List<Transform> children = new();
            foreach (Transform child in _seedsContainer.transform) { children.Add(child); }
            children.ForEach((t) => DestroyImmediate(t.gameObject));

            GameGrid grid = new(_seedContainerStart.position, _seedContainerEnd.position, 1, _props.MaxSeed);

            for (int r = 0; r < grid.Matrix.GetLength(0); r++)
            {
                for (int c = 0; c < grid.Matrix.GetLength(1); c++)
                {
                    GameObject cell = new($"GridCell{r}:{c}", typeof(BoxCollider2D));
                    cell.transform.parent = _seedsContainer;
                    cell.layer = LayerMask.NameToLayer("Selectable");
                    cell.transform.position = grid.Matrix[r, c];
                    cell.GetComponent<BoxCollider2D>().size = grid.CellSize;
                }
            }
        }
    }
}
