using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class SeedMenu : MonoBehaviour
    {
        [SerializeField] private Plant[] _plantPrefabs;

        private UnityEvent<Plant> _onItemClick;

        public void Awake()
        {
            Button[] buttons = GetComponentsInChildren<Button>();
            for (int i = 0; i < _plantPrefabs.Length; i++)
            {
                Plant plantPrefab = _plantPrefabs[i];
                SpriteRenderer plantSR = plantPrefab.GetComponent<SpriteRenderer>();
                Image buttonImg = buttons[i].GetComponent<Image>();
                buttonImg.sprite = plantSR.sprite;
                buttons[i].onClick.AddListener(() =>
                {
                    if (_onItemClick != null)
                    {
                        _onItemClick.Invoke(plantPrefab);
                    }
                });
            }
        }

        public void AddItemClickListener(UnityAction<Plant> listener)
        {
            if (_onItemClick == null)
            {
                _onItemClick = new UnityEvent<Plant>();
            }
            _onItemClick.AddListener(listener);
        }
    }
}
