using UnityEngine;

namespace Game
{
    public class Fire : PlantWeapon
    {
        [SerializeField] private FireProperties _fireProps;
        public override PlantWeaponProperties PlantWeaponProps => _fireProps.PlantWeaponProps;
    }
}
