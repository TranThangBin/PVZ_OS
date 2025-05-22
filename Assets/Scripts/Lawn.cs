using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Lawn : MonoBehaviour
{
    private UnityEvent<Vector2> _onLawnCellClick;

    public void Awake()
    {
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            RectTransform uiButton = button.GetComponentInChildren<RectTransform>();

            button.onClick.AddListener(() =>
            {
                Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, uiButton.position);
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
                _onLawnCellClick.Invoke(worldPos);
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
