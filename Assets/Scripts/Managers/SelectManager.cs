using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _selectables;
        [SerializeField] private GameObject[] _selectedObjects;
        [SerializeField] private Shovel _shovel;

        public UnityEvent<GameObject> OnItemSelect = new();

        private void Awake()
        {
            int preserveForShovel = 1;
            Button[] buttons = GetComponentsInChildren<Button>();

            for (int i = 0; i < _selectables.Length && i < buttons.Length - preserveForShovel; i++)
            {
                Button button = buttons[i];
                GameObject selected = _selectables[i];

                UpdateButton(button, selected);
            }

            UpdateButton(buttons[^1], _shovel.gameObject);
        }

        private void UpdateButton(Button button, GameObject selected)
        {
            SpriteRenderer plantSR = selected.GetComponent<SpriteRenderer>();
            Image buttonImg = button.GetComponent<Image>();
            buttonImg.sprite = plantSR.sprite;

            button.onClick.AddListener(() =>
            {
                OnItemSelect.Invoke(selected);
            });
        }
    }
}
