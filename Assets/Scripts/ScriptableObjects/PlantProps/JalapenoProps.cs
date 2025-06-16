using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Plant Properties/Jalapeno")]
    public class JalapenoProps : ScriptableObject
    {
        [field: SerializeField] public float DelayTime { get; private set; }
        [field: SerializeField] public Weapon Fire { get; private set; }
    }
}
