using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class MelonPult : ProjectileLauncher
    {
        protected override Tween Attack(GameObject projectile, Rigidbody2D target) => DOTween.
            Sequence().
            AppendCallback(() => Instantiate(projectile, transform.parent).
                GetComponent<Melon>().
                Targeting(target));
    }
}
