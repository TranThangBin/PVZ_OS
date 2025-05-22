using UnityEngine;

public class Pea : MonoBehaviour
{
    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.right * 5, ForceMode2D.Impulse);
    }
}
