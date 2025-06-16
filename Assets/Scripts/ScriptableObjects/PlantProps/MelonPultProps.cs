using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Melon Pult")]
    public class MelonPultProps : ScriptableObject
    {
        [field: SerializeField] public float ShootingInterval { get; private set; }
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public Melon Melon { get; private set; }
    }
}
