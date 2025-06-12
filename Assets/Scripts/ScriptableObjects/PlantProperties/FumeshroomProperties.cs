using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Fumeshroom Properties")]
    public class FumeshroomProperties : ScriptableObject
    {
        [SerializeField] private PlantProperties _plantProps;
        [SerializeField] private int _hp;
        [SerializeField] private float _shootingInterval;
        [SerializeField] private float _range;
        [SerializeField] private Fume _fume;

        public PlantProperties PlantProps => _plantProps;
        public int Hp => _hp;
        public float ShootingInterval => _shootingInterval;
        public float Range => _range;
        public Fume Fume => _fume;
    }
}
