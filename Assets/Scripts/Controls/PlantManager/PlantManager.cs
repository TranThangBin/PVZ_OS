using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PlantManager : MonoBehaviour
    {
        [SerializeField] private Transform _plantsParent;
        [SerializeField] private SeedMenu _seedMenu;
        [SerializeField] private Lawn _lawn;

        private GameObject _selectedPlant;

        public void Start()
        {
            _seedMenu.AddItemClickListener(_onItemClick);
            _lawn.AddLawnCellClickListener(_onLawnCellClick);
        }

        private void _onItemClick(GameObject plantPrefab)
        {
            if (_selectedPlant != plantPrefab)
            {
                _selectedPlant = plantPrefab;
            }
            else
            {
                _selectedPlant = null;
            }
        }

        private void _onLawnCellClick(Vector2 position)
        {
            if (_selectedPlant != null)
            {
                GameObject plant = Instantiate(_selectedPlant, position, Quaternion.identity, _plantsParent);
                _selectedPlant = null;
            }
        }
    }
}
