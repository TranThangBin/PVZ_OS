using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Peashooter : Attacker
    {
        protected override Tween Attack(GameObject projectile) => DOTween.
                Sequence().
                AppendCallback(() => Instantiate(projectile, transform.parent).
                    GetComponent<IProjectile>().
                    Fire(Vector2.right));
    }
}
