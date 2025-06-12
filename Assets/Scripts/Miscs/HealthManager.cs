using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    // if initial health is less than 0 then we go to infinity technically
    public class HealthManager : MonoBehaviour
    {
        private int _hp;
        private int Hp
        {
            get => _hp;
            set
            {
                _hp = value;
                if (_hp <= 0)
                {
                    OnOutOfHealth.Invoke(this);
                }
            }
        }

        private readonly UnityEvent<HealthManager> OnOutOfHealth = new();

        private void Start()
        {
            if (TryGetComponent(out IHealthy healthyObject)) { _hp = healthyObject.Health; }

            foreach (IDestroyOnOutOfHealth handler in GetComponents<IDestroyOnOutOfHealth>())
            {
                OnOutOfHealth.AddListener((sender) => Destroy(gameObject));
            }
        }

        public void ReduceHealth(int amount)
        {
            if (Hp > 0)
            {
                Hp -= amount;
            }
        }

        public interface IDestroyOnOutOfHealth : IHealthy { }

        public interface IHealthy
        {
            int Health { get; }
        }
    }
}