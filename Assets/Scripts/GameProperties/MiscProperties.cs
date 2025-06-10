using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Misc Properties")]
    public class MiscProperties : ScriptableObject
    {
        public int InitialSun;
        public SeedPacket SeedPacket;
        public Plant[] SeedBank;
    }
}
