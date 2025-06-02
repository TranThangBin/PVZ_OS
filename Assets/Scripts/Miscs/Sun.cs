using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Sun : MonoBehaviour
    {
        [SerializeField] private float _velocity;

        private float _targetYPos;

        private void Awake()
        {
            _targetYPos = transform.position.y;
        }

        private void Update()
        {
            if (transform.position.y > _targetYPos)
            {
                transform.Translate(_velocity * Time.deltaTime * Vector3.down);
            }
        }

        public void SetTargetYPosition(float yPos)
        {
            _targetYPos = yPos;
        }
    }
}
