using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Squash")]
    public class SquashProps : ScriptableObject
    {
        [field: SerializeField] public float JumpForce { get; private set; }
        [field: SerializeField] public float JumpTime { get; private set; }
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
    }
}
