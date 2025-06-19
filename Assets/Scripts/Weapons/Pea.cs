using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Weapon))]
    public class Pea : MonoBehaviour
    {
        [SerializeField] private PeaProps _props;

        private void OnDestroy() => DOTween.Kill(this);

        public void Targeting(Vector2 direction)
        {
            float flyDuration = 20;
            transform.
                DOMoveX(direction.x * _props.FlyVelocity * flyDuration, flyDuration).
                SetId(this);
        }
    }
}
