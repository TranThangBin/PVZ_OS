using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Chomper Properties")]
    public class ChomperProperties : ScriptableObject
    {
        [SerializeField] private PlantProperties _plantProps;
        [SerializeField] private int _hp;
        [SerializeField] private float _chewingInterval;
        [SerializeField] private int _damage;
        [SerializeField] private float _range;

        public PlantProperties PlantProps => _plantProps;
        public int Hp => _hp;
        public float ChewingInterval => _chewingInterval;
        public int Damage => _damage;
        public float Range => _range;
    }
}
