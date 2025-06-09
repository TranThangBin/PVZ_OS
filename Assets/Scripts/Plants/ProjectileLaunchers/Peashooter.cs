using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Peashooter : ProjectileLauncher
    {
        protected override Tween Attack(GameObject projectile, Rigidbody2D _) => DOTween.
            Sequence().
            AppendCallback(() => Instantiate(projectile).
                GetComponent<Pea>().
                Targeting(Vector2.right));
    }
}
