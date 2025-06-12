using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Melon Pult Properties")]
    public class MelonPultProperties : ScriptableObject
    {
        [SerializeField] private PlantProperties _plantProps;
        [SerializeField] private int _hp;
        [SerializeField] private float _shootingInterval;
        [SerializeField] private float _range;
        [SerializeField] private Melon _melon;

        public PlantProperties PlantProps => _plantProps;
        public int Hp => _hp;
        public float ShootingInterval => _shootingInterval;
        public float Range => _range;
        public Melon Melon => _melon;
    }
}
