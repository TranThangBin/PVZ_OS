using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class PlantManager : MonoBehaviour
    {
        [SerializeField] private SeedMenu _seedMenu;
        [SerializeField] private LawnManager _lawnManager;
        [SerializeField] private SunManager _sunManager;
        [SerializeField] private Transform _projectilePool;
        [SerializeField] private Image _selectedPlantIndicator;

        private Plant _selectedPlant;

        public void Start()
        {
            _seedMenu.AddItemClickListener(_onItemClick);
            _lawnManager.AddLawnCellClickListener(_onLawnCellClick);
        }

        private void _onItemClick(Plant plant)
        {
            if (_sunManager.Buyable(plant) && plant != _selectedPlant)
            {
                _setSelectedPlant(plant);
            }
            else
            {
                _setSelectedPlant(null);
            }
        }

        private void _onLawnCellClick(Transform transform)
        {
            if (transform.GetComponentInChildren<Plant>() != null)
            {
                return;
            }

            Plant buyedPlant = _sunManager.BuyPlant(_selectedPlant);

            if (buyedPlant != null)
            {
                Plant plant = Instantiate(buyedPlant, transform.position, Quaternion.identity, transform);
                plant.AddChildInstiateListener(_onPlantChildInstantiate);
            }

            _setSelectedPlant(null);
        }

        private void _onPlantChildInstantiate(GameObject child)
        {
            if (child.GetComponent<Sun>() != null)
            {
                child.transform.parent = _sunManager.transform;
            }
            else if (child.GetComponent<IProjectile>() != null)
            {
                child.transform.parent = _projectilePool;
            }
        }

        private void _setSelectedPlant(Plant plant)
        {
            Color cl = _selectedPlantIndicator.color;

            if (plant == null)
            {
                cl.a = 0;

                _selectedPlantIndicator.sprite = null;
                _selectedPlantIndicator.color = cl;
                _selectedPlant = null;
            }
            else
            {
                cl.a = 1;

                SpriteRenderer sr = plant.GetComponent<SpriteRenderer>();
                _selectedPlantIndicator.sprite = sr.sprite;
                _selectedPlantIndicator.color = cl;
                _selectedPlant = plant;
            }
        }
    }
}
