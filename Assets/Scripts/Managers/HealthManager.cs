using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] private float _hp;

        public UnityEvent OnOutOfHealth = new();

        public void Update()
        {
            if (IsOutOfHealth())
            {
                OnOutOfHealth.Invoke();
            }
        }

        public void ReduceHealth(float amount)
        {
            _hp = Mathf.Max(0, _hp - amount);
        }

        public void DestroyOnOutOfHealth()
        {
            Destroy(gameObject);
        }

        public bool IsOutOfHealth()
        {
            return _hp == 0;
        }
    }
}