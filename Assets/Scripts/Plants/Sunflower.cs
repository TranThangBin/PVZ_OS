using UnityEngine;

namespace Game
{
    public class Sunflower : MonoBehaviour
    {
        private enum SunflowerState { COOLDOWN, GENSUN };

        [SerializeField] private Sun _sun;
        [SerializeField] private Timer _timer;

        private SunflowerState _state = SunflowerState.COOLDOWN;

        private void Start()
        {
            _timer.TimerStart();
        }

        private void Update()
        {
            switch (_state)
            {
                case SunflowerState.GENSUN:
                    Instantiate(_sun, transform.position, Quaternion.identity, transform.parent);
                    _state = SunflowerState.COOLDOWN;
                    _timer.TimerRestart();
                    break;
                case SunflowerState.COOLDOWN:
                    break;
                default:
                    throw new UnityException();
            }
        }

        public void OnTimerTimeOut()
        {
            _state = SunflowerState.GENSUN;
        }
    }
}
