using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Game
{
    public abstract class Plant : MonoBehaviour,
        ILawnAction,
        HealthManager.IOnDamageTaken, HealthManager.IDestroyOnOutOfHealth
    {
        [SerializeField] private PlantID _plantID;
        [SerializeField] private PlantCosts _plantCosts;
        [SerializeField] private PlantCooldowns _plantCooldowns;

        public int Cost { get => _plantCosts.GetValue(_plantID); }
        public float Cooldown { get => _plantCooldowns.GetValue(_plantID); }
        public PlantID PlantID { get => _plantID; }

        public void ActionOnLawn(Transform location, UnityAction<GameObject, int> onSuccess)
        {
            if (location.GetComponentInChildren<Plant>() != null)
            {
                return;
            }
            onSuccess.Invoke(Instantiate(gameObject, location), Cost);
        }

        public virtual void OnDamageTaken(HealthManager sender)
        {
            SpriteRenderer sr = sender.GetComponent<SpriteRenderer>();
            Assert.IsNotNull(sr);
            sender.BlinkSpriteColor(sr);
        }
    }
}
