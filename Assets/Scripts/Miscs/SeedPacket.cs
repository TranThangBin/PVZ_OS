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
        private bool _onCooldown = false;
        private bool _enoughSun = false;

        private void OnDestroy() => DOTween.Kill(this);

        public void SetSelected(bool isSelected) => _srSelectedOverlay.enabled = isSelected;
        public bool IsAvailable() => _enoughSun && !_onCooldown;

        public void ActionOnLawn(Transform lawnCell, UnityAction<GameObject, int> onSuccess)
        {
            _plant.ActionOnLawn(lawnCell, (gameObj, cost) =>
            {
                onSuccess(gameObj, cost);
                DOTween.
                    Sequence(this).
                    AppendCallback(() =>
                    {
                        _onCooldown = true;
                        _cooldownOverlay.localScale = Vector2.one;
                    }).
                    Append(_cooldownOverlay.
                        DOScaleY(0, _plant.Cooldown).
                        SetEase(Ease.Linear)).
                    OnComplete(() => _onCooldown = false);
            });
        }

        public void SetPlant(Plant plant)
        {
            SpriteRenderer plantSprite = plant.GetComponent<SpriteRenderer>();
            _srPlantSprite.sprite = plantSprite.sprite;
            _srPlantSprite.size = new(0.5f, 0.5f);
            _tmPlantCost.text = plant.Cost.ToString();
            _plant = plant;
        }

        public void OnSunStoreChange(int sun)
        {
            _enoughSun = sun >= _plant.Cost;
            _srNotEnoughSunOverlay.enabled = !_enoughSun;
        }
    }
}
