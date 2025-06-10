using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(PlantWeapon))]
    public class Melon : MonoBehaviour
    {
        private void OnDestroy() => DOTween.Kill(this);

        public void Targeting(Rigidbody2D target)
        {
            PlantWeapon plantWeapon = GetComponent<PlantWeapon>();
            MelonProperties melonProps = plantWeapon.PlantWeaponsProps.Melon;
            float travelTime = Utils.CalculateTime(transform.position, target.position, melonProps.FlySpeed);
            DOTween.
                Sequence(this).
                Append(transform.
                    DOJump(target.position, melonProps.ThrowForce, 1, travelTime).
                    Join(transform.DORotate(Vector3.back * 90, travelTime)).
                    SetEase(Ease.Linear)).
                OnComplete(() => Destroy(gameObject));
        }
    }
}
