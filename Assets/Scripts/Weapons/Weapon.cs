using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponProps _props;

        private readonly UnityEvent<Collision2D> OnWeaponCollision = new();

        private void Awake()
        {
            foreach (IOnWeaponCollision c in GetComponents<IOnWeaponCollision>())
            {
                OnWeaponCollision.AddListener(c.OnWeaponCollision);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (CompareTag(collision.collider.tag) && collision.collider.TryGetComponent(out HealthManager healthManager))
            {
                healthManager.ReduceHealth(_props.Damage);
                OnWeaponCollision.Invoke(collision);
            }
        }

        public interface IOnWeaponCollision
        {
            void OnWeaponCollision(Collision2D collision);
        }
    }
}
