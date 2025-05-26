using UnityEngine;

public class Peashooter : MonoBehaviour
{
    [SerializeField] private Pea _pea;
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
            GameObject gameObject = Instantiate(_pea.gameObject, transform.position, Quaternion.identity);
            gameObject.GetComponent<IProjectile>().Fire(Vector2.right);
            _timer = _cooldown;
        }
    }
}
