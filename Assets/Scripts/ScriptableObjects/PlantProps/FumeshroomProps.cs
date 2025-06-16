using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Fumeshroom")]
    public class FumeshroomProps : ScriptableObject
    {
        [field: SerializeField] public float ShootingInterval { get; private set; }
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public Weapon Fume { get; private set; }
    }
}
