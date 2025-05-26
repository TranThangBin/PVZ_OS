using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class SeedMenu : MonoBehaviour
    {
        [SerializeField] private GameObject[] _plantPrefabs;

        private UnityEvent<GameObject> _onItemClick;

        public void Awake()
        {
            Button[] buttons = GetComponentsInChildren<Button>();
            for (int i = 0; i < _plantPrefabs.Length; i++)
            {
                GameObject plantPrefab = _plantPrefabs[i];
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

        public void AddItemClickListener(UnityAction<GameObject> listener)
        {
            if (_onItemClick == null)
            {
                _onItemClick = new UnityEvent<GameObject>();
            }
            _onItemClick.AddListener(listener);
        }
    }
}
