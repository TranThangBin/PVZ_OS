using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Seed Bank")]
    public class SeedBank : ScriptableObject
    {
        [SerializeField] private Plant[] _plants;

        public Plant[] Plants { get => _plants; }
    }
}
