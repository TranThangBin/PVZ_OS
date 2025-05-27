using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game
{
    public class Lawn : MonoBehaviour
    {
        private UnityEvent<Vector2> _onLawnCellClick;

        public void Awake()
        {
            foreach (Button button in GetComponentsInChildren<Button>())
            {
                button.onClick.AddListener(() =>
                {
                    _onLawnCellClick.Invoke(button.transform.position);
                });
            }
        }

        public void AddLawnCellClickListener(UnityAction<Vector2> listener)
        {
            if (_onLawnCellClick == null)
            {
                _onLawnCellClick = new UnityEvent<Vector2>();
            }
            _onLawnCellClick.AddListener(listener);
        }
    }
}
