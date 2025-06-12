using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Weapon Properties/Pea")]
    public class PeaProperties : ScriptableObject
    {
        [SerializeField] private PlantWeaponProperties _plantWeaponProps;
        [SerializeField] private float _flyVelocity;

        public PlantWeaponProperties PlantWeaponProps => _plantWeaponProps;
        public float FlyVelocity => _flyVelocity;
    }
}
