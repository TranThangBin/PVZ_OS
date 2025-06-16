using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Potato Mine")]
    public class PotatoMineProps : ScriptableObject
    {
        [field: SerializeField] public float PreparationTime { get; private set; }
        [field: SerializeField] public Weapon Armed { get; private set; }
    }
}
