using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Peashooter : Attacker
    {
        protected override Tween Attack(GameObject projectile)
        {
            return DOTween.
                Sequence().
                AppendCallback(() =>
                {
                    GameObject p = Instantiate(projectile, transform.parent);
                    p.GetComponent<IProjectile>().Fire(Vector2.right);
                });
        }
    }
}
