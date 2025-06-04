using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] private float _hp;
        private float Hp
        {
            get => Mathf.Max(0, _hp);
            set
            {
                _hp -= value;
                if (_hp == 0)
                {
                    OnOutOfHealth.Invoke();
                }
            }
        }

        public UnityEvent OnOutOfHealth = new();

        public void ReduceHealth(float amount)
        {
            Hp -= amount;
        }

        public void DestroyOnOutOfHealth()
        {
            Destroy(gameObject);
        }

        public bool IsOutOfHealth()
        {
            return Hp == 0;
        }
    }
}