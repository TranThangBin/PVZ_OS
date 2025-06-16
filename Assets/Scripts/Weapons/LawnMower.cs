using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Weapon))]
    public class LawnMower : MonoBehaviour, Weapon.IOnWeaponCollision
    {
        [SerializeField] private Rigidbody2D _rb;

        private void OnDestroy() => DOTween.Kill(this);

        public void OnWeaponCollision(Collision2D collision) => transform.
            DOMoveX(transform.position.x + 200, 10).
            OnComplete(() => Destroy(gameObject)).
            SetId(this);
    }
}