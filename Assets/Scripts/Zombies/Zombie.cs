using UnityEngine;
using UnityEngine.Assertions;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float _velocity;

    private Rigidbody2D _rb;

    public void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        Assert.IsNotNull(_rb, $"{typeof(Zombie)} require a {typeof(Rigidbody2D)}");
        Assert.IsTrue(_rb.bodyType == RigidbodyType2D.Kinematic, $"{typeof(Pea)} body should be kinematic");

        Assert.IsNotNull(GetComponent<HealthManager>(), $"{typeof(Zombie)} require a {typeof(HealthManager)}");
    }

    public void Start()
    {
        _rb.linearVelocity = _velocity * Vector2.left;
    }
}
