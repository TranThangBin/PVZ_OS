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
        [SerializeField] private SunManager _sunManager;
        [SerializeField] private Transform _plantPool;
        [SerializeField] private Transform _projectilePool;
        [SerializeField] private Image _selectedPlantIndicator;

        private Plant _selectedPlant;

        public void Start()
        {
            _seedMenu.AddItemClickListener(_onItemClick);
            _lawn.AddLawnCellClickListener(_onLawnCellClick);
        }

        private void _onItemClick(Plant plant)
        {
            if (_sunManager.Buyable(plant))
            {
                _setSelectedPlant(plant);
            }
            else
            {
                _setSelectedPlant(null);
            }
        }

        private void _onLawnCellClick(Vector2 position)
        {
            Plant buyedPlant = _sunManager.BuyPlant(_selectedPlant);

            if (buyedPlant != null)
            {
                Plant plant = Instantiate(buyedPlant, position, Quaternion.identity, _plantPool);
                plant.AddPlantAttackListener(_onPlantAttack);
            }

            _setSelectedPlant(null);
        }

        private void _onPlantAttack(GameObject projectile)
        {
            projectile.transform.parent = _projectilePool;
        }

        private void _setSelectedPlant(Plant plant)
        {
            if (plant == null)
            {
                _selectedPlantIndicator.sprite = null;
                _selectedPlant = null;
            }
            else
            {
                SpriteRenderer sr = plant.GetComponent<SpriteRenderer>();
                _selectedPlantIndicator.sprite = sr.sprite;
                _selectedPlant = plant;
            }
        }
    }
}
