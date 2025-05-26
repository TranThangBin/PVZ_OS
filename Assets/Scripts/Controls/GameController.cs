using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private SeedMenu _seedMenu;
    [SerializeField] private Lawn _lawn;

    private GameObject _selectedPlant;

    public void Start()
    {
        _seedMenu.AddItemClickListener(_onItemClick);
        _lawn.AddLawnCellClickListener(_onLawnCellClick);
    }

    private void _onItemClick(GameObject plantPrefab)
    {
        _selectedPlant = plantPrefab;
    }

    private void _onLawnCellClick(Vector2 position)
    {
        Instantiate(_selectedPlant, position, Quaternion.identity);
    }
}
