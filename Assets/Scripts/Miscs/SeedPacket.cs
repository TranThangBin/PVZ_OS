using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
namespace Game
{
    public class SeedPacket : MonoBehaviour, ISelectable, IAvailable
    {
        [SerializeField] private SpriteRenderer _srPlantSprite;
        [SerializeField] private SpriteRenderer _srSelectedOverlay;
        [SerializeField] private SpriteRenderer _srNotEnoughSunOverlay;
        [SerializeField] private TextMeshPro _tmPlantCost;
        [SerializeField] private Transform _cooldownOverlay;

        private Plant _plant;
        private bool _enoughSun;

        private void OnDestroy() => DOTween.Kill(this);

        private static bool IsProtector(Plant plant) => plant.gameObject.layer ==
            LayerMask.NameToLayer("Protector");

        public void ActionOnLawn(Transform lawnCell, UnityAction<int> onSuccess)
        {
            Plant[] plants = lawnCell.GetComponentsInChildren<Plant>();

            if (plants.Length == 2) { return; }

            if (plants.Length == 0 || plants.Any(IsProtector) != IsProtector(_plant))
            {
                DOTween.
                    Sequence(this).
                    AppendCallback(() =>
                    {
                        _cooldownOverlay.localScale = Vector2.one;
                        onSuccess.Invoke(_plant.Props.SunCost);
                        Instantiate(_plant.gameObject, lawnCell).tag = lawnCell.tag;
                    }).
                    Append(_cooldownOverlay.
                        DOScaleY(0, _plant.Props.SeedRechargeTime).
                        SetEase(Ease.Linear));
            }
        }

        public void SetPlant(Plant plant)
        {
            _plant = plant;

            _srPlantSprite.sprite = _plant.Props.Sprite;
            _srPlantSprite.size = new(0.5f, 0.5f);

            _tmPlantCost.text = _plant.Props.SunCost.ToString();
        }

        public void OnSunStoreChange(int sun)
        {
            _enoughSun = sun >= _plant.Props.SunCost;
            _srNotEnoughSunOverlay.enabled = !_enoughSun;
        }

        public void SetSelected(bool isSelected) => _srSelectedOverlay.enabled = isSelected;
        public bool IsAvailable() => _enoughSun && !DOTween.IsTweening(this);
    }
}
