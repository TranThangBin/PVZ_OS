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
        private bool _enoughSun = false;

        private void OnDestroy() => DOTween.Kill(this);

        public void ActionOnLawn(Transform lawnCell, UnityAction<int> onSuccess)
        {
            if (lawnCell.GetComponentInChildren<Plant>() == null)
            {
                onSuccess.Invoke(_plant.PlantProps.SunCost);
                Instantiate(_plant.gameObject, lawnCell);
                DOTween.
                    Sequence(this).
                    AppendCallback(() => _cooldownOverlay.localScale = Vector2.one).
                    Append(_cooldownOverlay.
                        DOScaleY(0, _plant.PlantProps.SeedRechargeTime).
                        SetEase(Ease.Linear));
            }
        }

        public void SetPlant(Plant plant)
        {
            _plant = plant;

            Sprite plantSprite = _plant.PlantProps.Sprite;
            _srPlantSprite.sprite = plantSprite;
            _srPlantSprite.size = new(0.5f, 0.5f);

            _tmPlantCost.text = _plant.PlantProps.SunCost.ToString();
        }

        public void OnSunStoreChange(int sun)
        {
            _enoughSun = sun >= _plant.PlantProps.SunCost;
            _srNotEnoughSunOverlay.enabled = !_enoughSun;
        }

        public void SetSelected(bool isSelected) => _srSelectedOverlay.enabled = isSelected;
        public bool IsAvailable() => _enoughSun && !DOTween.IsTweening(this);
    }
}
