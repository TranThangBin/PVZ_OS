using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class Plant : MonoBehaviour
    {
        [SerializeField] private int _plantCost;
        public int PlantCost { get => _plantCost; }
    }
}
