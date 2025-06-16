using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(HealthManager))]
    public class Plant : MonoBehaviour,
        HealthManager.IDestroyOnOutOfHealth, HealthManager.IBlinkOnDamageTaken
    {
        [field: SerializeField] public PlantProps Props { get; private set; }
        public int Health => Props.Hp;
        public Color BlinkColor => new(1, 0.25f, 0.25f);
        public SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();
    }
}
