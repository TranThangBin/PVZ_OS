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
                _hp = Mathf.Max(0, value);
                if (!_outOfHealthCalled && Hp == 0)
                {
                    OnOutOfHealth.Invoke();
                    _outOfHealthCalled = true;
                }
            }
        }

        public UnityEvent OnOutOfHealth = new();

        private bool _outOfHealthCalled = false;

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