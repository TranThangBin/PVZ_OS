using UnityEngine;

namespace Game
{
    [System.Serializable]
    public struct PlantProperties
    {
        [SerializeField] private int _sunCost;
        [SerializeField] private float _seedRechargeTime;
        [SerializeField] private Sprite _sprite;

        public readonly int SunCost => _sunCost;
        public readonly float SeedRechargeTime => _seedRechargeTime;
        public readonly Sprite Sprite => _sprite;
    }
}
