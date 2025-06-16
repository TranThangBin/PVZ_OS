using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Game
{
    public class HealthManager : MonoBehaviour
    {
        private int _hp;

        private readonly UnityEvent<HealthManager> OnOutOfHealth = new();
        private readonly UnityEvent<HealthManager> OnDamageTaken = new();

        private void OnValidate() =>
            Assert.IsTrue(TryGetComponent(out IHealthy health) && health.Health > 0,
                $"Expect an {typeof(IHealthy)} with more than 0 health");

        private void Start()
        {
            if (TryGetComponent(out IHealthy healthyObject)) { _hp = healthyObject.Health; }

            foreach (IDestroyOnOutOfHealth handler in GetComponents<IDestroyOnOutOfHealth>())
            {
                OnOutOfHealth.AddListener((sender) => Destroy(gameObject));
            }

            foreach (IOnDamageTaken handler in GetComponents<IOnDamageTaken>())
            {
                OnDamageTaken.AddListener(handler.OnDamageTaken);
            }
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

        public interface IDestroyOnOutOfHealth : IHealthy { }

        public interface IHealthy
        {
            int Health { get; }
        }
    }
}