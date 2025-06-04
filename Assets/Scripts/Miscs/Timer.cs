using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _time;
        [SerializeField] private bool _autoStart;

        public UnityEvent OnTimeOut = new();

        private bool _isStopped = true;
        private float _timerCounter;

        private void Awake()
        {
            _timerCounter = _time;
        }

        private void Start()
        {
            if (_autoStart)
            {
                TimerStart();
            }
        }

        private void Update()
        {
            if (_isStopped)
            {
                return;
            }

            SetTimeCounter(_timerCounter - Time.deltaTime);
            if (_timerCounter == 0)
            {
                _isStopped = true;
                OnTimeOut.Invoke();
            }
        }

        private void SetTimeCounter(float time)
        {
            _timerCounter = Mathf.Max(0, time);
        }

        public void TimerRestart()
        {
            _timerCounter = _time;
            _isStopped = false;
        }

        public void TimerStart()
        {
            if (_timerCounter == _time)
            {
                _isStopped = false;
            }
        }

        public void TimerStop()
        {
            _isStopped = true;
        }

        public bool TimerIsPaused()
        {
            return _isStopped && _timerCounter != _time;
        }

        public void TimerReset()
        {
            _timerCounter = _time;
        }

        public bool TimerIsStopped()
        {
            return _isStopped;
        }

        public float TimerGetTime()
        {
            return _timerCounter;
        }

        public void TimerSetTime(float time)
        {
            _time = time;
            _timerCounter = time;
        }

        public float TimerGetPercentage()
        {
            if (_time == 0)
            {
                return 0;
            }
            return 1 - _timerCounter / _time;
        }

        public void DestroyOnTimeOut()
        {
            Destroy(gameObject);
        }
    }
}
