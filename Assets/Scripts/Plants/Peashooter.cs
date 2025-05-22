using UnityEngine;

public class Peashooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _cooldown;
    private float _timer;

    public void Start()
    {
        _timer = _cooldown;
    }

    public void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            Instantiate(_projectile, transform.position, Quaternion.identity);
            _timer = _cooldown;
        }
    }
}
