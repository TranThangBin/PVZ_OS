using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Fumeshroom : WeaponizedPlant
    {
        protected override WeaponizedPlantProperties WeaponizedPlantProps => PlantsProps.Fumeshroom;

        protected override Tween Attack(GameObject projectile, Rigidbody2D target)
        {
            return DOTween.
                Sequence().
                AppendCallback(() => Instantiate(projectile, transform.parent));
        }
    }
}
