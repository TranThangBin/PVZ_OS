using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(PlantWeapon))]
    public class Fire : MonoBehaviour, PlantWeapon.IPlantWeapon
    {
        [SerializeField] private FireProperties _fireProps;
        public PlantWeaponProperties PlantWeaponProps => _fireProps.PlantWeaponProps;
    }
}
