using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Zombie Properties/Basic Zombie")]
    public class BasicZombieProps : ScriptableObject
    {
        [field: SerializeField] public int Hp { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float AttackCooldown { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
    }
}
