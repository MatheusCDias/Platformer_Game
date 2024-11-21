using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig2D;
    private SpriteRenderer sprite;
    private Animator animator;
    public GameObject point;

    public float speed;
    public float jumpForce;
    private float b = 1;
    private float h = .57f;

    private bool isJumping;
    private bool doubleJump;
    private bool isAttacking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        point = GameObject.Find("Point");
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Attack();
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
            if (!isJumping && !isAttacking)
                animator.SetInteger("Transition", 1);
            sprite.flipX = true;
        }
        else if (movement > 0)
        {
            if (!isJumping && !isAttacking)
                animator.SetInteger("Transition", 1);
            sprite.flipX = false;
        }
        else if (movement == 0)
        {
            if (!isJumping && !isAttacking)
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

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isAttacking = true;
            Collider2D hit = Physics2D.OverlapBox(point.transform.position, new Vector2 (b, h), 0);
            animator.SetInteger("Transition", 4);
            StartCoroutine("OnAttack");
        }        
    }

    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(0.183f);
        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(point.transform.position, new Vector3(b, h, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isJumping = false;
        }
    }
}