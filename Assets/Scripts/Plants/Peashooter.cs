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
            GameObject gameObject = Instantiate(_projectile, transform.position, Quaternion.identity);
            IProjectile projectile = gameObject.GetComponent<IProjectile>();
            projectile.Fire(Vector2.right);
            _timer = _cooldown;
        }
    }
}
