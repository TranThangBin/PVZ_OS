using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Misc/Seed Bank Properties")]
    public class SeedBankProps : ScriptableObject
    {
        [field: SerializeField] public SeedPacket SeedPacket { get; private set; }
        [field: SerializeField] public Plant[] Seeds { get; private set; }
        [field: SerializeField] public int MaxSeed { get; private set; }
        [field: SerializeField] public int InitialSun { get; private set; }
    }
}