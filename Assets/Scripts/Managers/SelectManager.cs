using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _selectables;

        public UnityEvent<GameObject> OnItemSelect = new();

        private void Awake()
        {
            Button[] buttons = GetComponentsInChildren<Button>();
            for (int i = 0; i < _selectables.Length && i < buttons.Length; i++)
            {
                GameObject selected = _selectables[i];
                SpriteRenderer plantSR = selected.GetComponent<SpriteRenderer>();
                Image buttonImg = buttons[i].GetComponent<Image>();
                buttonImg.sprite = plantSR.sprite;

                buttons[i].onClick.AddListener(() =>
                {
                    OnItemSelect.Invoke(selected);
                });
            }
        }
    }
}
