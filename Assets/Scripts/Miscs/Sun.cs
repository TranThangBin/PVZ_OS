using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Sun : MonoBehaviour
    {
        [SerializeField] private float _velocity;
        [SerializeField] private float _lifeTime;

        private float _targetYPos;
        private UnityEvent _onSunClick;

        public void Update()
        {
            if (transform.position.y > _targetYPos)
            {
                transform.Translate(_velocity * Time.deltaTime * Vector3.down);
                return;
            }

            if (_lifeTime > 0)
            {
                _lifeTime -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
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
