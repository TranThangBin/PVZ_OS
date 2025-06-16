using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Sunflower")]
    public class SunflowerProps : ScriptableObject
    {
        [field: SerializeField] public float SunGenerateInterval { get; private set; }
        [field: SerializeField] public Sun Sun { get; private set; }
    }
}
