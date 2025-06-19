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
            foreach (IOnWeaponCollisionEnter c in GetComponents<IOnWeaponCollisionEnter>())
            {
                OnWeaponCollision.AddListener(c.OnWeaponCollisionEnter);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (CompareTag(collision.collider.tag) && collision.collider.TryGetComponent(out HealthManager healthManager))
            {
                healthManager.ReduceHealth(_props.Damage);
                OnWeaponCollision.Invoke(collision);
                if (_props.DestroyOnCollision)
                {
                    Destroy(gameObject);
                }
            }
        }

        public interface IOnWeaponCollisionEnter
        {
            void OnWeaponCollisionEnter(Collision2D collision);
        }
    }
}
