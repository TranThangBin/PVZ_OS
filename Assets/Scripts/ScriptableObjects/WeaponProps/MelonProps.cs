using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Weapon Properties/Melon")]
    public class MelonProps : ScriptableObject
    {
        [field: SerializeField] public float FlyTime { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
    }
}
