using UnityEngine;

namespace Game
{
    public class SeedPacket : MonoBehaviour, ISelectable
    {
        [SerializeField] private SpriteRenderer _srPlantSprite;
        [SerializeField] private TextMesh _tmPlantCost;

        private Plant _plant;

        public bool ActionOnLocation(Transform location, SunManager sunManager)
        {
            return _plant.ActionOnLocation(location, sunManager);
        }

        public bool CanSelect(SunManager sunManager)
        {
            return _plant.CanSelect(sunManager);
        }

        public void SetPlant(Plant plant)
        {
            SpriteRenderer plantSprite = plant.GetComponent<SpriteRenderer>();
            _srPlantSprite.sprite = plantSprite.sprite;
            _srPlantSprite.size = new Vector2(0.5f, 0.5f);
            _tmPlantCost.text = plant.GetPlantCost().ToString();
            _plant = plant;
        }
    }
}
