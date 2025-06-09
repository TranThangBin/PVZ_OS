using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Fumeshroom : ProjectileLauncher
    {
        protected override Tween Attack(GameObject projectile, Rigidbody2D target)
        {
            return DOTween.
                Sequence().
                AppendCallback(() => Instantiate(projectile, transform.parent));
        }
    }
}
