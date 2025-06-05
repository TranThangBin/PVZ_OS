using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public abstract class Plant : MonoBehaviour,
        ILawnAction,
        HealthManager.IOnDamageTaken, HealthManager.IDestroyOnOutOfHealth
    {
        [SerializeField] private int _cost;
        [SerializeField] private float _cooldown;

        public void ActionOnLawn(Transform location, UnityAction<GameObject, int> onSuccess)
        {
            if (location.GetComponentInChildren<Plant>() != null)
            {
                return;
            }
            onSuccess.Invoke(Instantiate(gameObject, location), _cost);
        }

        public int GetCost()
        {
            return _cost;
        }

        public float GetCooldown()
        {
            return _cooldown;
        }

        public virtual void OnDamageTaken(HealthManager sender)
        {
            SpriteRenderer sr = sender.GetComponent<SpriteRenderer>();
            Assert.IsNotNull(sr);
            sender.BlinkSpriteColor(sr);
        }
    }
}
