using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class SplitPea : ProjectileLauncher
    {
        protected override ProjectileLauncherProperties ProjectileLauncherProps => PlantsProps.SplitPea;

        protected override Tween Attack(GameObject projectile, Rigidbody2D _) => DOTween.
                Sequence().
                AppendCallback(() => InstantiatePea(projectile).Targeting(Vector2.right)).
                AppendInterval(0.5f).
                AppendCallback(() => InstantiatePea(projectile).Targeting(Vector2.left)).
                AppendInterval(0.2f).
                AppendCallback(() => InstantiatePea(projectile).Targeting(Vector2.left));

        protected override RaycastHit2D Raycast()
        {
            float rayLength = ProjectileLauncherProps.VisionLength;

            return Utils.Raycast(
                transform.position + Vector3.left * rayLength,
                Vector2.right, rayLength * 2, LayerMask.GetMask("Enemy"), Color.red);
        }

        private Pea InstantiatePea(GameObject projectile)
        {
            return Instantiate(
                projectile, transform.position, Quaternion.identity, transform.parent
            ).GetComponent<Pea>();
        }
    }
}
