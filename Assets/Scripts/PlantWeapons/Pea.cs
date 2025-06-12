using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Pea : PlantWeapon
    {
        [SerializeField] private PeaProperties _peaProps;

        private void OnDestroy() => DOTween.Kill(this);

        public void Targeting(Vector2 target)
        {
            float flyTime = Utils.CalculateTime(transform.position, target, _peaProps.FlyVelocity);
            transform.
                DOMove(target, flyTime).
                SetEase(Ease.Linear).
                OnComplete(() => Destroy(gameObject)).
                SetId(this);
        }

        public override PlantWeaponProperties PlantWeaponProps => _peaProps.PlantWeaponProps;
    }
}
