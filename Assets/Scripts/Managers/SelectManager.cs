using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField] private Plant[] _selectables;

        public UnityEvent<Plant> OnItemSelect = new();

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
                    OnItemSelect.Invoke(_selectable);
                });
            }
        }
    }
}
