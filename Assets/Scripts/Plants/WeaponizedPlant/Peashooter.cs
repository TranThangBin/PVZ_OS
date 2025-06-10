using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Peashooter : WeaponizedPlant
    {
        protected override WeaponizedPlantProperties WeaponizedPlantProps => PlantsProps.Peashooter;

        protected override Tween Attack(GameObject projectile, Rigidbody2D _) => DOTween.
            Sequence().
            AppendCallback(() => Instantiate(projectile, transform.parent).
                GetComponent<Pea>().
                Targeting(Vector2.right));
    }
}
