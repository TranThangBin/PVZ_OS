using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class MelonPult : WeaponizedPlant
    {
        protected override WeaponizedPlantProperties WeaponizedPlantProps => PlantsProps.MelonPult;

        protected override Tween Attack(GameObject projectile, Rigidbody2D target) => DOTween.
            Sequence().
            AppendCallback(() => Instantiate(projectile, transform.parent).
                GetComponent<Melon>().
                Targeting(target));
    }
}
