using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Weapon Properties/Fire")]
    public class FireProperties : ScriptableObject
    {
        [SerializeField] private PlantWeaponProperties _plantWeaponProps;

        public PlantWeaponProperties PlantWeaponProps => _plantWeaponProps;
    }
}
