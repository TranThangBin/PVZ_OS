using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Game
{
    public class Sun : MonoBehaviour
    {
        [SerializeField] private float _velocity;
        [SerializeField] private Timer _lifeTimer;

        private float _targetYPos;

        public void Awake()
        {
            _targetYPos = transform.position.y;
        }

        public void Start()
        {
            _lifeTimer.TimerStart();
        }

        public void Update()
        {
            if (transform.position.y > _targetYPos)
            {
                transform.Translate(_velocity * Time.deltaTime * Vector3.down);
            }
        }

        public void SetTargetYPosition(float yPos)
        {
            Assert.IsTrue(transform.position.y > yPos);
            _targetYPos = yPos;
        }

        public void OnTimerTimeOut()
        {
            Destroy(gameObject);
        }
    }
}
