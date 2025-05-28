using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PlantManager : MonoBehaviour
    {
        [SerializeField] private SelectManager _seedMenu;
        [SerializeField] private LawnManager _lawnManager;
        [SerializeField] private SunManager _sunManager;
        [SerializeField] private Transform _projectilePool;
        [SerializeField] private Image _selectedPlantIndicator;

        private Plant _selected;

        public void Start()
        {
            _seedMenu.AddItemClickListener(_onItemClick);
            _lawnManager.AddLawnCellClickListener(_onLawnCellClick);
        }

        private void _onItemClick(Plant plant)
        {
            if (_sunManager.Buyable(plant) && plant != _selected)
            {
                _setSelected(plant);
            }
            else
            {
                _setSelected(null);
            }
        }

        private void _onLawnCellClick(Transform cell)
        {
            if (cell.GetComponentInChildren<Selectable>() != null)
            {
                return;
            }

            Plant bought = _sunManager.BuyPlant(_selected);

            if (bought != null)
            {
                Plant plant = Instantiate(bought, cell.position, Quaternion.identity, cell);
            }

            _setSelected(null);
        }

        private void _setSelected(Plant selectable)
        {
            Color cl = _selectedPlantIndicator.color;

            if (selectable == null)
            {
                cl.a = 0;

                _selectedPlantIndicator.sprite = null;
                _selectedPlantIndicator.color = cl;
                _selected = null;
            }
            else
            {
                cl.a = 1;

                SpriteRenderer sr = selectable.GetComponent<SpriteRenderer>();
                _selectedPlantIndicator.sprite = sr.sprite;
                _selectedPlantIndicator.color = cl;
                _selected = selectable;
            }
        }
    }
}
