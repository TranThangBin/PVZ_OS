using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/General")]
    public class PlantProps : ScriptableObject
    {
        [field: SerializeField] public int SunCost { get; private set; }
        [field: SerializeField] public float SeedRechargeTime { get; private set; }
        [field: SerializeField] public int Hp { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}
