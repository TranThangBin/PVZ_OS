using UnityEngine;

namespace Game
{
    public class ArmedPotatoMine : PlantWeapon
    {
        [SerializeField] private ArmedPotatoMineProperties _armedPotatoMineProps;
        public override PlantWeaponProperties PlantWeaponProps => _armedPotatoMineProps.PlantWeaponProps;
    }
}
