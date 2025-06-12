using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(PlantWeapon))]
    public class Pea : MonoBehaviour, PlantWeapon.IPlantWeapon
    {
        [SerializeField] private PeaProperties _peaProps;

        private void OnDestroy() => DOTween.Kill(this);

        public void Targeting(Vector2 target)
        {
            float flyTime = Utils.CalculateTime(transform.position, target, _peaProps.FlyVelocity);
            transform.DOMove(target, flyTime).OnComplete(() => Destroy(gameObject)).SetId(this);
        }

        public PlantWeaponProperties PlantWeaponProps => _peaProps.PlantWeaponProps;
    }
}
