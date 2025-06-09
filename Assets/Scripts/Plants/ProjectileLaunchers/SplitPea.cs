using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class SplitPea : ProjectileLauncher
    {
        protected override Tween Attack(GameObject projectile, Rigidbody2D _) => DOTween.
                Sequence().
                AppendCallback(() => InstantiatePea(projectile).Targeting(Vector2.right)).
                AppendInterval(0.5f).
                AppendCallback(() => InstantiatePea(projectile).Targeting(Vector2.left)).
                AppendInterval(0.2f).
                AppendCallback(() => InstantiatePea(projectile).Targeting(Vector2.left));

        private Pea InstantiatePea(GameObject projectile)
        {
            return Instantiate(
                projectile, transform.position, Quaternion.identity, transform.parent
            ).GetComponent<Pea>();
        }
    }
}
