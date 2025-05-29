using TMPro;
using UnityEngine;

namespace Game
{
    public class SunManager : MonoBehaviour
    {
        [SerializeField] private Sun _sun;
        [SerializeField] private TMP_Text _sunDisplay;
        [SerializeField] private Transform _sunSpawnStart;
        [SerializeField] private Transform _sunSpawnEnd;
        [SerializeField] private Timer _sunSpawnTimer;
        [SerializeField] private int _sunStored = 50;

        private void Awake()
        {
            _sunDisplay.text = _sunStored.ToString();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 1, LayerMask.GetMask("Sun"));

                if (hit.collider != null)
                {
                    Destroy(hit.collider.gameObject);
                    IncrementSunStore(25);
                }
            }
        }

        public void OnTimerTimeOut()
        {
            int pad = 5;
            Vector3 spawnPos = new Vector2(Random.Range(_sunSpawnStart.position.x, _sunSpawnEnd.position.x),
                _sunSpawnStart.position.y + pad);

            Sun sun = Instantiate(_sun, spawnPos, Quaternion.identity, transform);
            sun.SetTargetYPosition(Random.Range(_sunSpawnStart.position.y, _sunSpawnEnd.position.y));

            _sunSpawnTimer.TimerRestart();
        }

        public void IncrementSunStore(int amount)
        {
            _sunStored += amount;
            _sunDisplay.text = _sunStored.ToString();
        }

        public void DecrementSunStore(int amount)
        {
            _sunStored -= amount;
            _sunDisplay.text = _sunStored.ToString();
        }

        public bool Buyable(int cost)
        {
            return _sunStored >= cost;
        }
    }
}
