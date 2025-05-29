using UnityEngine;

namespace Game
{
    public class Plant : MonoBehaviour
    {
        [SerializeField] private int _plantCost;
        public int PlantCost { get => _plantCost; }
    }
}
