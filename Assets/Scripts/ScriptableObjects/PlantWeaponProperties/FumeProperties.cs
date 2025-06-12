using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Weapon Properties/Fume")]
    public class FumeProperties : ScriptableObject
    {
        [SerializeField] private PlantWeaponProperties _plantWeaponProps;

        public PlantWeaponProperties PlantWeaponProps => _plantWeaponProps;
    }
}
