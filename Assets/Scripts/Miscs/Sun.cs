using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Sun : MonoBehaviour
    {
        [SerializeField] private float _velocity;
        [SerializeField] private float _lifeTime;

        private float _targetYPos;

        public void Awake()
        {
            _targetYPos = transform.position.y;
        }

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

        public void SetTargetYPosition(float yPos)
        {
            _targetYPos = yPos;
        }
    }
}
