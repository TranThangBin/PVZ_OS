using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField] private Plant[] _selectables;

        private UnityEvent<Plant> _onItemClick;

        public void Awake()
        {
            Button[] buttons = GetComponentsInChildren<Button>();
            for (int i = 0; i < _selectables.Length && i < buttons.Length; i++)
            {
                Plant _selectable = _selectables[i];
                SpriteRenderer plantSR = _selectable.GetComponent<SpriteRenderer>();
                Image buttonImg = buttons[i].GetComponent<Image>();
                buttonImg.sprite = plantSR.sprite;
                buttons[i].onClick.AddListener(() =>
                {
                    if (_onItemClick != null)
                    {
                        _onItemClick.Invoke(_selectable);
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
