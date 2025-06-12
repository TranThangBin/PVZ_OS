using DG.Tweening;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Plant))]
    public class PotatoMine : MonoBehaviour, Plant.IPlant, HealthManager.IDestroyOnOutOfHealth
    {
        [SerializeField] private PotatoMineProperties _potatoMineProps;

        private void OnDestroy() => DOTween.Kill(this);

        private void Start() =>
            DOTween.
                Sequence(this).
                AppendInterval(_potatoMineProps.PreparationTime).
                AppendCallback(() =>
                {
                    Instantiate(_potatoMineProps.ArmedPotatoMine, transform.parent);
                    Destroy(gameObject);
                });

        public PlantProperties PlantProps => _potatoMineProps.PlantProps;

        public int Health => _potatoMineProps.Hp;
    }
}
