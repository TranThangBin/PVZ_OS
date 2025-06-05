using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] private float _hp;
        private float Hp
        {
            get => Mathf.Max(0, _hp);
            set
            {
                _hp = Mathf.Max(0, value);
                if (IsOutOfHealth())
                {
                    _onOutOfHealth.Invoke(this);
                }
            }
        }

        private readonly UnityEvent<HealthManager> _onOutOfHealth = new();
        private readonly UnityEvent<HealthManager> _onDamageTaken = new();

        private Color _blinkColor = new(1, 0.5f, 0.5f);

        private void Start()
        {
            foreach (var handler in GetComponents<IOnOutOfHealth>())
            {
                _onOutOfHealth.AddListener(handler.OnOutOfHealth);
            }
            foreach (var handler in GetComponents<IDestroyOnOutOfHealth>())
            {
                _onOutOfHealth.AddListener((sender) => Destroy(gameObject));
            }
            foreach (var handler in GetComponents<IOnDamageTaken>())
            {
                _onDamageTaken.AddListener(handler.OnDamageTaken);
            }
        }

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }

        public void ReduceHealth(float amount)
        {
            if (!IsOutOfHealth())
            {
                Hp -= amount;
                _onDamageTaken.Invoke(this);
            }
        }

        public void BlinkSpriteColor(SpriteRenderer sr)
        {
            DOTween.Kill(this);
            sr.
                DOColor(_blinkColor, 0.1f).
                SetLoops(2, LoopType.Yoyo).
                OnKill(() => sr.color = Color.white).
                SetId(this);
        }

        public bool IsOutOfHealth()
        {
            return Hp == 0;
        }

        public void SetBlinkColor(Color cl)
        {
            _blinkColor = cl;
        }

        public interface IDestroyOnOutOfHealth { }

        public interface IOnOutOfHealth
        {
            void OnOutOfHealth(HealthManager sender);
        }

        public interface IOnDamageTaken
        {
            void OnDamageTaken(HealthManager sender);
        }
    }
}