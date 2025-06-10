using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Pea : PlantWeapon
    {
        private PeaProperties PeaProps => PlantWeaponsProps.Pea;

        protected override PlantWeaponProperties PlantWeaponProps => PeaProps.PlantWeaponProps;

        public void Targeting(Vector2 direction) =>
            GetComponent<Rigidbody2D>().linearVelocity = PeaProps.FlySpeed * direction;
    }
}
