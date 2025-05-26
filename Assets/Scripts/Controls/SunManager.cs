using System.Linq;
using TMPro;
using UnityEngine;

namespace Game
{
    public class SunManager : MonoBehaviour
    {
        [SerializeField] private Sun _sun;
        [SerializeField] private TMP_Text _sunDisplay;
        [SerializeField] private Transform _sunSpanwTopLeft;
        [SerializeField] private Transform _sunSpanwBottomRight;
        [SerializeField] private int _sunStored = 50;
        [SerializeField] private float _sunSpawnTimer;

        private float _timer;

        public void Awake()
        {
            _sunDisplay.text = _sunStored.ToString();
            _timer = _sunSpawnTimer;
        }

        public void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                int pad = 5;
                Vector3 spawnPos = new Vector2(Random.Range(_sunSpanwTopLeft.position.x, _sunSpanwBottomRight.position.x),
                    _sunSpanwTopLeft.position.y + pad);
                Sun sun = Instantiate(_sun, spawnPos, Quaternion.identity, transform);

                sun.AddSunClickListener(_onSunClick);

                sun.SetTargetYPosition(Random.Range(_sunSpanwTopLeft.position.y, _sunSpanwBottomRight.position.y));
                _timer = _sunSpawnTimer;
            }
        }

        private void _onSunClick()
        {
            _incrementSunStore(25);
        }

        private void _incrementSunStore(int amount)
        {
            _sunStored += amount;
            _sunDisplay.text = _sunStored.ToString();
        }

        private void _decrementSunStore(int amount)
        {
            _sunStored -= amount;
            _sunDisplay.text = _sunStored.ToString();
        }

        public bool Buyable(Plant plant)
        {
            return plant != null && _sunStored >= plant.PlantCost;
        }

        public Plant BuyPlant(Plant plant)
        {
            if (!Buyable(plant))
            {
                return null;
            }
            _decrementSunStore(plant.PlantCost);
            return plant;
        }
    }
}
