using UnityEngine;
using UnityEngine.Events;
namespace Game
{
    public class SeedPacket : MonoBehaviour, IValuable, ISelectable
    {
        [SerializeField] private SpriteRenderer _srPlantSprite;
        [SerializeField] private SpriteRenderer _srSelectedOverlay;
        [SerializeField] private TextMesh _tmPlantCost;

        private Plant _plant;

        public int GetValue()
        {
            return _plant.GetValue();
        }

        public void ActionOnLawn(Transform lawnCell, UnityAction<GameObject> onSuccess)
        {
            _plant.ActionOnLawn(lawnCell, onSuccess);
        }

        public void SetPlant(Plant plant)
        {
            SpriteRenderer plantSprite = plant.GetComponent<SpriteRenderer>();
            _srPlantSprite.sprite = plantSprite.sprite;
            _srPlantSprite.size = new Vector2(0.5f, 0.5f);
            _tmPlantCost.text = plant.GetValue().ToString();
            _plant = plant;
        }

        public void SetSelected(bool isSelected)
        {
            _srSelectedOverlay.enabled = isSelected;
        }

        public Plant GetPlant()
        {
            return _plant;
        }
    }
}
