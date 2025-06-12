using UnityEngine;

namespace Game
{
    public class Fume : PlantWeapon
    {
        [SerializeField] private FumeProperties _fumeProps;
        public override PlantWeaponProperties PlantWeaponProps => _fumeProps.PlantWeaponProps;
    }
}
