using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _selectables;
        [SerializeField] private Shovel _shovel;
        [SerializeField] private SunManager _sunManager;
        [SerializeField] private LawnManager _lawnManager;

        private GameObject _selected;

        private void Awake()
        {
            int preserveForShovel = 1;
            Button[] buttons = GetComponentsInChildren<Button>();

            for (int i = 0; i < _selectables.Length && i < buttons.Length - preserveForShovel; i++)
            {
                Button button = buttons[i];
                GameObject selected = _selectables[i];

                UpdateButton(button, selected);
            }

            UpdateButton(buttons[^1], _shovel.gameObject);

            _lawnManager.OnLawnCellClick.AddListener(OnLawnCellClick);
        }

        private void UpdateButton(Button button, GameObject selected)
        {
            SpriteRenderer selectedSr = selected.GetComponent<SpriteRenderer>();

            Image buttonImg = button.GetComponent<Image>();
            buttonImg.sprite = selectedSr.sprite;

            ISelectable selectable = selected.GetComponent<ISelectable>();

            button.onClick.AddListener(() =>
            {
                if (_selected == selected)
                {
                    _selected = null;
                }
                else if (selectable.CanSelect(_sunManager))
                {
                    _selected = selected;
                }
            });
        }

        private void OnLawnCellClick(Transform cell)
        {
            if (_selected == null)
            {
                return;
            }

            ISelectable selectable = _selected.GetComponent<ISelectable>();

            if (selectable.ActionOnLocation(cell, _sunManager))
            {
                _selected = null;
            }
        }
    }
}
