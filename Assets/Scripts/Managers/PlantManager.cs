using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PlantManager : MonoBehaviour
    {
        [SerializeField] private SelectManager _selectManager;
        [SerializeField] private LawnManager _lawnManager;
        [SerializeField] private SunManager _sunManager;
        [SerializeField] private Image _selectedIndicator;

        private Plant _selected;

        public void Start()
        {
            _selectManager.OnItemSelect.AddListener(OnItemSelect);
            _lawnManager.OnLawnCellClick.AddListener(OnLawnCellClick);
        }

        private void OnItemSelect(Plant plant)
        {
            if (_sunManager.Buyable(plant) && plant != _selected)
            {
                SetSelected(plant);
            }
            else
            {
                SetSelected(null);
            }
        }

        private void OnLawnCellClick(Transform cell)
        {
            if (cell.GetComponentInChildren<Plant>() != null)
            {
                return;
            }

            Plant bought = _sunManager.BuyPlant(_selected);

            if (bought != null)
            {
                Instantiate(bought, cell.position, Quaternion.identity, cell);
            }

            SetSelected(null);
        }

        private void SetSelected(Plant selectable)
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
