using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class PotatoMine : Plant, HealthManager.IDestroyOnOutOfHealth
    {
        [SerializeField] private PotatoMineProperties _potatoMineProps;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start() =>
            DOTween.
                Sequence(this).
                AppendInterval(_potatoMineProps.PreparationTime).
                AppendCallback(() =>
                {
                    ArmedPotatoMine armed = Instantiate(_potatoMineProps.ArmedPotatoMine, transform.parent);
                    armed.gameObject.layer = gameObject.layer;
                    Destroy(gameObject);
                });

        public override PlantProperties PlantProps => _potatoMineProps.PlantProps;

        public int Health => _potatoMineProps.Hp;
    }
}
