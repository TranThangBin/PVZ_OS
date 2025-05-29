using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using static Codice.Client.BaseCommands.Import.Commit;

namespace Game
{
    public class PlantManager : MonoBehaviour
    {
        [SerializeField] private SelectManager _selectManager;
        [SerializeField] private LawnManager _lawnManager;
        [SerializeField] private SunManager _sunManager;
        [SerializeField] private Image _selectedIndicator;

        private GameObject _selected;

        private void Start()
        {
            _selectManager.OnItemSelect.AddListener(OnItemSelect);
            _lawnManager.OnLawnCellClick.AddListener(OnLawnCellClick);
        }

        private void OnItemSelect(GameObject selected)
        {
            ISelectable selectable = selected.GetComponent<ISelectable>();
            Assert.IsNotNull(selectable);

            if (selectable.CanSelect(_sunManager))
            {
                SetSelected(selected);
                return;
            }
            SetSelected(null);
        }

        private void OnLawnCellClick(Transform cell)
        {
            if (_selected == null)
            {
                return;
            }

            ISelectable selectable = _selected.GetComponent<ISelectable>();
            Assert.IsNotNull(selectable);

            if (selectable.ActionOnLocation(cell, _sunManager))
            {
                SetSelected(null);
            }
        }

        private void SetSelected(GameObject selectable)
        {
            Color cl = _selectedIndicator.color;

            if (selectable == null)
            {
                cl.a = 0;

                _selectedIndicator.sprite = null;
                _selectedIndicator.color = cl;
                _selected = null;
            }
            else
            {
                cl.a = 1;

                SpriteRenderer sr = selectable.GetComponent<SpriteRenderer>();
                _selectedIndicator.sprite = sr.sprite;
                _selectedIndicator.color = cl;
                _selected = selectable;
            }
        }
    }
}
