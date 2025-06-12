using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Weapon Properties/Armed Potato Mine")]
    public class ArmedPotatoMineProperties : ScriptableObject
    {
        [SerializeField] private PlantWeaponProperties _plantWeaponProps;

        public PlantWeaponProperties PlantWeaponProps => _plantWeaponProps;
    }
}
