using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Squash Properties")]
    public class SquashProperties : ScriptableObject
    {
        [SerializeField] private PlantProperties _plantProps;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _range;
        [SerializeField] private int _damage;

        public PlantProperties PlantProps => _plantProps;
        public float JumpForce => _jumpForce;
        public float JumpTime => _jumpTime;
        public float Range => _range;
        public int Damage => _damage;
    }
}
