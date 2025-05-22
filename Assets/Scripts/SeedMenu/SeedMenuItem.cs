using UnityEngine;
using UnityEngine.UI;

public class SeedMenuItem : MonoBehaviour
{
    [SerializeField] private GameObject _plantPrefab;
    public GameObject PlantPrefab { get => _plantPrefab; }

    public void Start()
    {
        Image buttonImage = GetComponent<Image>();
        buttonImage.sprite = _plantPrefab.GetComponent<SpriteRenderer>().sprite;
    }
}
