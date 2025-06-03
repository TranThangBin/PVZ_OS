using UnityEngine;

namespace Game
{
    public class Sunflower : Plant
    {
        [SerializeField] private Sun _sun;
        [SerializeField] private Timer _rechargeTimer;

        private void Start()
        {
            _rechargeTimer.TimerStart();
        }

        public void OnTimerTimeOut()
        {
            Instantiate(_sun, transform.parent);
            _rechargeTimer.TimerRestart();
        }
    }
}
