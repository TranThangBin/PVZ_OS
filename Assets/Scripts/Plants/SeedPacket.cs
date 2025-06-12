using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
namespace Game
{
    public class SeedPacket : MonoBehaviour, ISelectable, IAvailable
    {
        [SerializeField] private SpriteRenderer _srPlantSprite;
        [SerializeField] private SpriteRenderer _srSelectedOverlay;
        [SerializeField] private SpriteRenderer _srNotEnoughSunOverlay;
        [SerializeField] private TextMesh _tmPlantCost;
        [SerializeField] private Transform _cooldownOverlay;

        private Plant _plant;
        private PlantProperties _plantProperties;
        private bool _onCooldown = false;
        private bool _enoughSun = false;

        private void OnDestroy() => DOTween.Kill(this);

        public void SetSelected(bool isSelected) => _srSelectedOverlay.enabled = isSelected;
        public bool IsAvailable() => _enoughSun && !_onCooldown;

        public void ActionOnLawn(Transform lawnCell, UnityAction<GameObject, int> onSuccess)
        {
            _plant.Planting(lawnCell, (gameObj) =>
            {
                onSuccess.Invoke(gameObj, _plantProperties.SunCost);
                DOTween.
                    Sequence(this).
                    AppendCallback(() =>
                    {
                        _onCooldown = true;
                        _cooldownOverlay.localScale = Vector2.one;
                    }).
                    Append(_cooldownOverlay.
                        DOScaleY(0, _plantProperties.SeedRechargeTime).
                        SetEase(Ease.Linear)).
                    OnComplete(() => _onCooldown = false);
            });
        }

        public void SetPlant(Plant plant)
        {
            _plant = plant;
            _plantProperties = _plant.GetComponent<Plant.IPlant>().PlantProps;

            Sprite plantSprite = _plantProperties.Sprite;
            _srPlantSprite.sprite = plantSprite;
            _srPlantSprite.size = new(0.5f, 0.5f);

            _tmPlantCost.text = _plantProperties.SunCost.ToString();
        }

        public void OnSunStoreChange(int sun)
        {
            _enoughSun = sun >= _plantProperties.SunCost;
            _srNotEnoughSunOverlay.enabled = !_enoughSun;
        }
    }
}
