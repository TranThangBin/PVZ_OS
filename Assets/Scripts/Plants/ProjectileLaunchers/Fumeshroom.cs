using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Fumeshroom : ProjectileLauncher
    {
        protected override ProjectileLauncherProperties ProjectileLauncherProps => PlantsProps.Fumeshroom;

        protected override Tween Attack(GameObject projectile, Rigidbody2D target)
        {
            return DOTween.
                Sequence().
                AppendCallback(() => Instantiate(projectile, transform.parent));
        }
    }
}
