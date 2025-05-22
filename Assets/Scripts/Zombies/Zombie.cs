using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float _velocity;
    [SerializeField] private float hp;

    public void Update()
    {
        transform.position += _velocity * Time.deltaTime * Vector3.left;
    }
}
