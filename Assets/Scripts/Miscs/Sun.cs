using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Sun : MonoBehaviour
    {
        [SerializeField] private float _velocity;

        private float _targetYPos;
        private UnityEvent _onSunClick;

        public void Update()
        {
            if (transform.position.y > _targetYPos)
            {
                transform.Translate(_velocity * Time.deltaTime * Vector3.down);
            }
        }

        public void AddSunClickListener(UnityAction listener)
        {
            if (_onSunClick == null)
            {
                _onSunClick = new UnityEvent();
            }
            _onSunClick.AddListener(listener);
        }

        public void OnMouseDown()
        {
            if (_onSunClick != null)
            {
                _onSunClick.Invoke();
                Destroy(gameObject);
            }
        }

        public void SetTargetYPosition(float yPos)
        {
            _targetYPos = yPos;
        }
    }
}
