using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Melon : MonoBehaviour
    {
        [SerializeField] private PlantWeaponsProperties _plantWeaponsProps;

        private MelonProperties MelonProps => _plantWeaponsProps.Melon;

        private void OnDestroy() => DOTween.Kill(this);

        public void Targeting(Rigidbody2D target)
        {
            float travelTime = Utils.CalculateTime(transform.position, target.position, MelonProps.FlySpeed);
            DOTween.
                Sequence(this).
                Append(transform.
                    DOJump(target.position, MelonProps.ThrowForce, 1, travelTime).
                    Join(transform.DORotate(Vector3.back * 90, travelTime)).
                    SetEase(Ease.Linear)).
                OnComplete(() => Destroy(gameObject));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out HealthManager zombieHealth))
            {
                zombieHealth.ReduceHealth(MelonProps.PlantWeaponProps.Damage);
                Destroy(gameObject);
            }
        }
    }
}
