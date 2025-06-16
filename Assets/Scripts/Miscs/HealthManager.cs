using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Game
{
    public class HealthManager : MonoBehaviour
    {
        private int _hp;

        private readonly UnityEvent<HealthManager> OnOutOfHealth = new();
        private readonly UnityEvent<HealthManager> OnDamageTaken = new();

        private void OnValidate() =>
            Assert.IsTrue(TryGetComponent(out IHealthy health) && health.Health > 0,
                $"Expect an {typeof(IHealthy)} with more than 0 health");

        private void Start()
        {
            if (TryGetComponent(out IHealthy healthyObject)) { _hp = healthyObject.Health; }
            if (TryGetComponent(out IDestroyOnOutOfHealth _)) { OnOutOfHealth.AddListener((sender) => Destroy(gameObject)); }
            if (TryGetComponent(out IBlinkOnDamageTaken blinker))
            {
                OnDamageTaken.AddListener((sender) => Blink(blinker.SpriteRenderer, blinker.BlinkColor));
            }

        }

        private void Blink(SpriteRenderer sprite, Color color)
        {
            sprite.
                DOColor(color, 0.1f).
                SetLoops(2, LoopType.Yoyo).
                SetId(this);
        }

        public void ReduceHealth(int amount)
        {
            if (_hp > 0)
            {
                _hp = Mathf.Max(0, _hp - amount);

                if (_hp == 0)
                {
                    OnOutOfHealth.Invoke(this);
                    _hp = -1;
                }
                else
                {
                    OnDamageTaken.Invoke(this);
                }
            }
        }

        public bool IsOutOfHealth() => _hp == 0 || _hp == -1;

        public interface IDestroyOnOutOfHealth : IHealthy { }

        public interface IBlinkOnDamageTaken : IHealthy
        {
            Color BlinkColor { get; }
            SpriteRenderer SpriteRenderer { get; }
        }

        public interface IHealthy
        {
            int Health { get; }
        }
    }
}