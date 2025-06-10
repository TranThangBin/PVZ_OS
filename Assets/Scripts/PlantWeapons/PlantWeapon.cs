using UnityEngine;

namespace Game
{
    public class PlantWeapon : MonoBehaviour
    {
        [SerializeField] private MiscProperties _gameProps;

        private float _initialYPos;

        private void Start() => _initialYPos = transform.position.y;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            float errorMargin = 2;
            if (collision.transform.position.y <= _initialYPos + errorMargin &&
                collision.transform.position.y >= _initialYPos - errorMargin)
            {
            }
        }
    }
}
