using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(PlantWeapon))]
    public class ArmedPotatoMine : MonoBehaviour, PlantWeapon.IPlantWeapon
    {
        [SerializeField] private ArmedPotatoMineProperties _armedPotatoMineProps;
        public PlantWeaponProperties PlantWeaponProps => _armedPotatoMineProps.PlantWeaponProps;
    }
}
