using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _time;

        public UnityEvent OnTimeOut = new();

        private bool _isStopped = true;
        private float _counter;

        public void Awake()
        {
            _counter = _time;
        }

        public void Update()
        {
            if (_isStopped)
            {
                return;
            }

            _counter -= Time.deltaTime;
            if (_counter <= 0)
            {
                _isStopped = true;
                OnTimeOut.Invoke();
            }
        }

        public void TimerRestart()
        {
            _counter = _time;
            _isStopped = false;
        }

        public void TimerStart()
        {
            if (_counter == _time)
            {
                _isStopped = false;
            }
        }

        public void TimerReset()
        {
            _counter = _time;
        }

        public bool TimerIsStopped()
        {
            return _isStopped;
        }

        public void DestroyOnTimeOut()
        {
            Destroy(gameObject);
        }
    }
}
