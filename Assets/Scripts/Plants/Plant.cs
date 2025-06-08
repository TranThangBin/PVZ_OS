using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Game
{
    public abstract class Plant : MonoBehaviour,
        HealthManager.IOnDamageTaken, HealthManager.IDestroyOnOutOfHealth
    {
        [SerializeField] private PlantID _plantID;

        public PlantID PlantID { get => _plantID; }

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
            Assert.IsNotNull(sr);
            sender.BlinkSpriteColor(sr);
        }
    }
}
