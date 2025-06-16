using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Weapon Properties/Pea")]
    public class PeaProps : ScriptableObject
    {
        [field: SerializeField] public float FlyVelocity { get; private set; }
    }
}
