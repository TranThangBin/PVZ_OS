using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Weapon))]
    public class LawnMower : MonoBehaviour, Weapon.IOnWeaponCollisionEnter
    {
        private void OnDestroy() => DOTween.Kill(this);

        public void OnWeaponCollisionEnter(Collision2D collision) => transform.
            DOMoveX(transform.position.x + 200, 7).
            OnComplete(() => Destroy(gameObject)).
            SetId(this);
    }
}