using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Weapon Properties/Melon")]
    public class MelonProperties : ScriptableObject
    {
        [SerializeField] private PlantWeaponProperties _plantWeaponProps;
        [SerializeField] private float _flyTime;
        [SerializeField] private float _jumpForce;

        public PlantWeaponProperties PlantWeaponProps => _plantWeaponProps;
        public float FlyTime => _flyTime;
        public float JumpForce => _jumpForce;
    }
}
