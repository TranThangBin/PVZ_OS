using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class HealthManager : MonoBehaviour
    {
        private int _hp;

        private readonly UnityEvent<HealthManager> OnOutOfHealth = new();
        private readonly UnityEvent<HealthManager> OnDamageTaken = new();

        private void Start()
        {
            if (TryGetComponent(out IHealthy healthyObject)) { _hp = healthyObject.Health; }

            if (TryGetComponent(out IInfiniteHealth _)) { _hp = -2; }

            foreach (IDestroyOnOutOfHealth handler in GetComponents<IDestroyOnOutOfHealth>())
            {
                OnOutOfHealth.AddListener((sender) => Destroy(gameObject));
            }

            foreach (IOnDamageTaken handler in GetComponents<IOnDamageTaken>())
            {
                OnDamageTaken.AddListener(handler.OnDamageTaken);
            }

            if (_hp == 0) { Debug.LogWarning(gameObject + " does not have any health initializer"); }
        }

        public void ReduceHealth(int amount)
        {
            if (_hp > 0)
            {
                _hp = Mathf.Max(0, _hp - amount);

                if (_hp == 0)
                {
                    OnOutOfHealth.Invoke(this);
                    _hp = -1;
                }
            }
        }

        public bool IsOutOfHealth() => _hp == 0 || _hp == -1;

        public interface IOnDamageTaken
        {
            void OnDamageTaken(HealthManager sender);
        }

        public interface IInfiniteHealth { }

        public interface IDestroyOnOutOfHealth : IHealthy { }

        public interface IHealthy
        {
            int Health { get; }
        }
    }
}