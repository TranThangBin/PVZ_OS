using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Zombie Properties/Basic Zombie")]
    public class BasicZombieProperties : ScriptableObject
    {
        [SerializeField] private int _hp;
        [SerializeField] private int _damage;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _movementSpeed;

        public int Hp => _hp;
        public int Damage => _damage;
        public float AttackCooldown => _attackSpeed;
        public float AttackRange => _attackRange;
        public float MovementSpeed => _movementSpeed;
    }
}
