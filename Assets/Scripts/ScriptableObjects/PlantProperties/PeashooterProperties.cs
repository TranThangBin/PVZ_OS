using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Peashooter Properties")]
    public class PeashooterProperties : ScriptableObject
    {
        [SerializeField] private PlantProperties _plantProps;
        [SerializeField] private int _hp;
        [SerializeField] private float _shootingInterval;
        [SerializeField] private float _range;
        [SerializeField] private Pea _pea;

        public PlantProperties PlantProps => _plantProps;
        public int Hp => _hp;
        public float ShootingInterval => _shootingInterval;
        public float Range => _range;
        public Pea Pea => _pea;
    }
}
