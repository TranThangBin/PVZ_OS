using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    public class PotatoMine : Plant
    {
        [SerializeField] private PlantChargeTimes _plantChargeTimes;
        [SerializeField] private PlantDamages _plantDamages;
        [SerializeField] private GameObject _unarmed;
        [SerializeField] private GameObject _armed;

        private GameObject _activeState;
        private GameObject ActiveState
        {
            get => _activeState;
            set
            {
                if (_activeState != null)
                {
                    _activeState.SetActive(false);
                }
                _activeState = value;
                _activeState.SetActive(true);
            }
        }

        private void Awake()
        {
            ActiveState = _unarmed;
        }

        private void Start()
        {
            DOTween.
                Sequence(this).
                AppendInterval(_plantChargeTimes.GetValue(PlantID)).
                AppendCallback(() => ActiveState = _armed);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (ActiveState == _armed && collision.gameObject.TryGetComponent(out HealthManager zombieHealth))
            {
                zombieHealth.ReduceHealth(_plantDamages.GetValue(PlantID));
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }

        public override void OnDamageTaken(HealthManager sender)
        {
            SpriteRenderer sr = ActiveState.GetComponent<SpriteRenderer>();
            Assert.IsNotNull(sr);
            sender.BlinkSpriteColor(sr);
        }
    }
}
