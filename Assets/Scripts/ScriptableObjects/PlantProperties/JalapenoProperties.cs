using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Jalapeno Properties")]
    public class JalapenoProperties : ScriptableObject
    {
        [SerializeField] private PlantProperties _plantProps;
        [SerializeField] private int _hp;
        [SerializeField] private float _delayTime;
        [SerializeField] private Fire _fire;

        public PlantProperties PlantProps => _plantProps;
        public int Hp => _hp;
        public float DelayTime => _delayTime;
        public Fire Fire => _fire;
    }
}
