using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SeedMenu : MonoBehaviour
{
    private UnityEvent<GameObject> _onItemClick;

    public void Awake()
    {
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            SeedMenuItem seedMenuItem = button.GetComponentInChildren<SeedMenuItem>();
            button.onClick.AddListener(() =>
            {
                if (_onItemClick != null)
                {
                    _onItemClick.Invoke(seedMenuItem.PlantPrefab);
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
