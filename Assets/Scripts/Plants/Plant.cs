using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public abstract class Plant : MonoBehaviour,
        HealthManager.IOnDamageTaken, HealthManager.IDestroyOnOutOfHealth
    {
        [SerializeField] private PlantsProperties _plantsProps;

        protected PlantsProperties PlantsProps => _plantsProps;
        public abstract PlantProperties PlantProps { get; }

        private void Awake() => gameObject.AddComponent<HealthManager>().InitHealth(PlantProps.Health);

        public void Planting(Transform location, UnityAction<GameObject> onSuccess)
        {
            if (location.GetComponentInChildren<Plant>() != null)
            {
                return;
            }
            onSuccess.Invoke(Instantiate(gameObject, location));
        }

        public virtual void OnDamageTaken(HealthManager sender)
        {
            SpriteRenderer sr = sender.GetComponent<SpriteRenderer>();
            sender.BlinkSpriteColor(sr);
        }
    }
}
