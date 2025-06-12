using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Misc Properties")]
    public class MiscProperties : ScriptableObject
    {
        [SerializeField] private int _initialSun;
        [SerializeField] private Plant[] _seedBank;
        [SerializeField] private SeedPacket _seedPacket;
        [SerializeField] private Sun _sun;
        [SerializeField] private BasicZombie _basicZombie;
        [SerializeField] private float _sunSpawnInterval;
        [SerializeField] private float _zombieSpawnInterval;
        [SerializeField] private int _zombieSpawnRows;

        public int InitialSun => _initialSun;
        public Plant[] SeedBank => _seedBank;
        public SeedPacket SeedPacket => _seedPacket;
        public Sun Sun => _sun;
        public BasicZombie BasicZombie => _basicZombie;
        public float SunSpawnInterval => _sunSpawnInterval;
        public float ZombieSpawnInterval => _zombieSpawnInterval;
        public int ZombieSpawnRows => _zombieSpawnRows;
    }
}
