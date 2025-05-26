using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PlantManager : MonoBehaviour
    {
        [SerializeField] private SeedMenu _seedMenu;
        [SerializeField] private Lawn _lawn;
        [SerializeField] private Transform _plantPool;
        [SerializeField] private Transform _projectilePool;

        private Plant _selectedPlant;

        public void Start()
        {
            _seedMenu.AddItemClickListener(_onItemClick);
            _lawn.AddLawnCellClickListener(_onLawnCellClick);
        }

        private void _onItemClick(Plant plantPrefab)
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
                Plant plant = Instantiate(_selectedPlant, position, Quaternion.identity, _plantPool);
                plant.AddPlantAttackListener(_onPlantAttack);

                _selectedPlant = null;
            }
        }

        private void _onPlantAttack(GameObject projectile)
        {
            projectile.transform.parent = _projectilePool;
        }
    }
}
