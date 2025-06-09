using DG.Tweening;
using UnityEngine;

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

        private void OnDestroy() => DOTween.Kill(this);

        private void Start()
        {
            _activeState = _unarmed;
            DOTween.
                Sequence(this).
                AppendInterval(_plantChargeTimes.GetValue(PlantID)).
                AppendCallback(() => ActiveState = _armed);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (ActiveState == _armed && collision.collider.TryGetComponent(out HealthManager zombieHealth))
            {
                zombieHealth.ReduceHealth(_plantDamages.GetValue(PlantID));
                Destroy(gameObject);
            }
        }

        public override void OnDamageTaken(HealthManager sender)
        {
            SpriteRenderer sr = ActiveState.GetComponent<SpriteRenderer>();
            sender.BlinkSpriteColor(sr);
        }
    }
}
