using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant/Plant Ranges")]
    public class PlantRanges : PlantValues<PlantRange> { }

    [System.Serializable]
    public struct PlantRange
    {
        [SerializeField] private float _range;
        [SerializeField] private bool _bothDirection;

        public readonly float Range { get => _range; }
        public readonly bool BothDirection { get => _bothDirection; }
    }
}
