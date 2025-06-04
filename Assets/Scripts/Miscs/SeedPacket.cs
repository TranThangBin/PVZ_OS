using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
namespace Game
{
    public class SeedPacket : MonoBehaviour, IValuable, ISelectable, IAvailable
    {
        [SerializeField] private SpriteRenderer _srPlantSprite;
        [SerializeField] private SpriteRenderer _srSelectedOverlay;
        [SerializeField] private TextMesh _tmPlantCost;
        [SerializeField] private Transform _cooldownOverlay;

        private Plant _plant;
        private bool _isAvailable = true;

        public int GetValue()
        {
            return _plant.GetValue();
        }

        public void ActionOnLawn(Transform lawnCell, UnityAction<GameObject> onSuccess)
        {
            _plant.ActionOnLawn(lawnCell, onSuccess);
            _isAvailable = false;

            _cooldownOverlay.localScale = Vector2.one;
            _cooldownOverlay.
                DOScaleY(0, _plant.GetCooldown()).
                SetEase(Ease.Linear).
                OnComplete(() => _isAvailable = true);
        }

        public void SetPlant(Plant plant)
        {
            SpriteRenderer plantSprite = plant.GetComponent<SpriteRenderer>();
            _srPlantSprite.sprite = plantSprite.sprite;
            _srPlantSprite.size = new(0.5f, 0.5f);
            _tmPlantCost.text = plant.GetValue().ToString();
            _plant = plant;
        }

        public void SetSelected(bool isSelected)
        {
            _srSelectedOverlay.enabled = isSelected;
        }

        public bool IsAvailable()
        {
            return _isAvailable;
        }
    }
}
