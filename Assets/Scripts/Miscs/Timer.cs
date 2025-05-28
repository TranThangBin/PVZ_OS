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
                OnTimeOut.Invoke();
                _isStopped = true;
            }
        }

        public void TimerStart()
        {
            if (_time > 0)
            {
                _isStopped = false;
            }
        }

        public void TimerRestart()
        {
            _counter = _time;
            _isStopped = false;
        }

        public bool TimerIsStopped()
        {
            return !_isStopped;
        }
    }
}
