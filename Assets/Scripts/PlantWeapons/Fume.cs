using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(PlantWeapon))]
    public class Fume : MonoBehaviour, PlantWeapon.IPlantWeapon
    {
        [SerializeField] private FumeProperties _fumeProps;
        public PlantWeaponProperties PlantWeaponProps => _fumeProps.PlantWeaponProps;
    }
}
