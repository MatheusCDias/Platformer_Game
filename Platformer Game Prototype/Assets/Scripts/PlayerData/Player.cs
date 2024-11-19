using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig2D;
    private SpriteRenderer sprite;

    public float speed;
    public float jumpForce;
    public bool isJumping;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        float movement = Input.GetAxis("Horizontal");

        rig2D.linearVelocity = new Vector2(movement * speed, rig2D.linearVelocityY);

        if (movement < 0)
        {
            sprite.flipX = true;
        }
        else if (movement > 0)
        {
            sprite.flipX = false;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rig2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isJumping = false;
        }
    }
}
