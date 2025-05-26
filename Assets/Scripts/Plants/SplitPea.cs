using System.Collections;
using UnityEngine;

public class SplitPea : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _shotDelay;
    private float _timer;
    private Vector2[] _fireDirections;

    public void Start()
    {
        _timer = _cooldown;
        _fireDirections = new Vector2[] { Vector2.right, Vector2.left, Vector2.left };
    }

    public void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            StartCoroutine(_fireProjectilesWithDelay());
            _timer = _cooldown;
        }
    }

    private IEnumerator _fireProjectilesWithDelay()
    {
        foreach (Vector2 direction in _fireDirections)
        {
            GameObject gameObject = Instantiate(_projectile, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_shotDelay);
        }
    }
}
