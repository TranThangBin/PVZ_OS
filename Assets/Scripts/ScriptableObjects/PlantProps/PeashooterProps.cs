using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Peashooter")]
    public class PeashooterProps : ScriptableObject
    {
        [field: SerializeField] public float ShootingInterval { get; private set; }
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public Pea Pea { get; private set; }
    }
}
