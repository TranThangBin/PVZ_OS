using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Potato Mine Properties")]
    public class PotatoMineProperties : ScriptableObject
    {
        [SerializeField] private PlantProperties _plantProps;
        [SerializeField] private int _hp;
        [SerializeField] private float _preparationTime;
        [SerializeField] private PlantWeapon _armedPotatoMine;

        public PlantProperties PlantProps => _plantProps;
        public int Hp => _hp;
        public float PreparationTime => _preparationTime;
        public PlantWeapon ArmedPotatoMine => _armedPotatoMine;
    }
}
