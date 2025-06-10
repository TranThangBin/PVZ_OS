using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Object/Game Properties")]
    public class GameProperties : ScriptableObject
    {
        public int InitialSun;
        public SeedPacket SeedPacket;
        public Plant[] SeedBank;
        public PlantsProperties Plants;
        public PlantWeaponsProperties PlantWeapons;
        public ZombiesProperties Zombies;
    }
}
