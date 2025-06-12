using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Sunflower")]
    public class SunflowerProperties : ScriptableObject
    {
        [SerializeField] private PlantProperties _plantProps;
        [SerializeField] private int _hp;
        [SerializeField] private float _sunGenerateInterval;
        [SerializeField] private Sun _sun;

        public PlantProperties PlantProps => _plantProps;
        public int Hp => _hp;
        public float SunGenerateInterval => _sunGenerateInterval;
        public Sun Sun => _sun;
    }
}
