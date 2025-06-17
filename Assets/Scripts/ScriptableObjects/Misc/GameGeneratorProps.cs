using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Misc/Game Generator Properties")]
    public class GameGeneratorProps : ScriptableObject
    {
        [field: SerializeField] public LawnMower[] Cleaners { get; private set; }
        [field: SerializeField] public int LawnColumns { get; private set; }
        [field: SerializeField] public int SunValue { get; private set; }
        [field: SerializeField] public Sun Sun { get; private set; }
        [field: SerializeField] public float SunSpawnInterval { get; private set; }
    }
}
