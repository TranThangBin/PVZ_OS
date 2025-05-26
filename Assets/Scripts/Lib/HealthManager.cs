using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float _hp;

    public float Hp { get => _hp; }

    public void Update()
    {
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ReduceHealth(float amount)
    {
        _hp = Mathf.Max(0, _hp - amount);
    }
}
