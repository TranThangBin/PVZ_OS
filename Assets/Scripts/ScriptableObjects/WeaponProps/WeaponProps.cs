using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Weapon Properties/General")]
    public class WeaponProps : ScriptableObject
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public bool DestroyOnCollision { get; private set; }
    }
}
