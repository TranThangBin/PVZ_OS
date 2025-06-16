using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Chomper")]
    public class ChomperProps : ScriptableObject
    {
        [field: SerializeField] public float ChewingInterval { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float Range { get; private set; }
    }
}
