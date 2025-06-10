using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D), typeof(PlantWeapon))]
    public class Pea : MonoBehaviour
    {
        public void Targeting(Vector2 direction)
        {
            PlantWeapon weapon = GetComponent<PlantWeapon>();
            float flySpeed = weapon.PlantWeaponsProps.Pea.FlySpeed;
            GetComponent<Rigidbody2D>().linearVelocity = flySpeed * direction;
        }
    }
}
