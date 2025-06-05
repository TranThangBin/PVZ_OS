using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class SplitPea : Attacker
    {
        protected override Tween Attack(GameObject projectile)
        {
            return DOTween.
                Sequence().
                AppendCallback(() => ShootPea(Vector2.right, projectile)).
                AppendInterval(0.5f).
                AppendCallback(() => ShootPea(Vector2.left, projectile)).
                AppendInterval(0.2f).
                AppendCallback(() => ShootPea(Vector2.left, projectile));
        }

        private void ShootPea(Vector3 direction, GameObject projectile)
        {
            GameObject p = Instantiate(projectile, transform.position + direction / 2, Quaternion.identity, transform.parent);
            p.GetComponent<IProjectile>().Fire(direction);
        }
    }
}
