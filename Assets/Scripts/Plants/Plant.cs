using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public abstract class Plant : MonoBehaviour,
        HealthManager.IOnDamageTaken, HealthManager.IDestroyOnOutOfHealth
    {
        [SerializeField] private PlantID _plantID;
        [SerializeField] private PlantHealths _plantHealths;

        public PlantID PlantID { get => _plantID; }

        private void Awake() => gameObject.AddComponent<HealthManager>().InitHealth(_plantHealths.GetValue(_plantID));

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
