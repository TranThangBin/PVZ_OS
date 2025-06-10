using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Melon : MonoBehaviour
    {
        [SerializeField] private GameProperties _gameProps;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _jumpForce;

        private MelonProperties MelonProps => _gameProps.PlantWeapons.Melon;

        private void OnDestroy() => DOTween.Kill(this);

        public void Targeting(Rigidbody2D target)
        {
            float travelTime = Utils.CalculateTime(transform.position, target.position, MelonProps.FlySpeed);
            DOTween.
                Sequence(this).
                Append(transform.
                    DOJump(target.position, _jumpForce, 1, travelTime).
                    Join(transform.DORotate(Vector3.back * 90, travelTime)).
                    SetEase(Ease.OutBack));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out HealthManager zombieHealth))
            {
                zombieHealth.ReduceHealth(MelonProps.Damage);
                Destroy(gameObject);
            }
        }
    }
}
