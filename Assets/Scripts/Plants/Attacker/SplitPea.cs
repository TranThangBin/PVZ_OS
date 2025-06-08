using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class SplitPea : Attacker
    {
        protected override Tween Attack(GameObject projectile) => DOTween.
                Sequence().
                AppendCallback(() => InstantiatePea(projectile).Fire(Vector2.right)).
                AppendInterval(0.5f).
                AppendCallback(() => InstantiatePea(projectile).Fire(Vector2.left)).
                AppendInterval(0.2f).
                AppendCallback(() => InstantiatePea(projectile).Fire(Vector2.left));

        private IProjectile InstantiatePea(GameObject projectile)
        {
            return Instantiate(
                projectile, transform.position, Quaternion.identity, transform.parent
            ).GetComponent<IProjectile>();
        }
    }
}
