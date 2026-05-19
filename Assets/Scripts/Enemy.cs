using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private Rigidbody2D rb2D;
    private bool movingRight = true;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.linearVelocity = new Vector2(speed, 0);
    }

    void Update()
    {
        float direction = movingRight ? 1f : -1f;
        rb2D.linearVelocity = new Vector2(direction * speed, rb2D.linearVelocity.y);
        transform.localScale = new Vector3(direction, 1, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            movingRight = !movingRight;
        }
    }
}