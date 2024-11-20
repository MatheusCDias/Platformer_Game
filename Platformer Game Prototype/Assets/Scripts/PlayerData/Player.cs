using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig2D;
    private SpriteRenderer sprite;
    private Animator animator;

    public float speed;
    public float jumpForce;

    private bool isJumping;
    private bool doubleJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
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
            if (!isJumping)
                animator.SetInteger("Transition", 1);
            sprite.flipX = true;
        }
        else if (movement > 0)
        {
            if (!isJumping)
                animator.SetInteger("Transition", 1);
            sprite.flipX = false;
        }
        else if (movement == 0 && !isJumping)
        {
            animator.SetInteger("Transition", 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                rig2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetInteger("Transition", 2);
                isJumping = true;
                doubleJump = true;
            }
            else if (doubleJump)
            {
                rig2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                animator.SetInteger("Transition", 3);
                doubleJump = false;
            }
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