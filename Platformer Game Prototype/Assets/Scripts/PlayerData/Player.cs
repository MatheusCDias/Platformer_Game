using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Components
    private Rigidbody2D rig2D;
    private SpriteRenderer sprite;
    private Animator animator;

    // Configurable Parameters
    [Header("Player Stats")]
    [SerializeField, Range(0, 5)] private int health = 5;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Attack Settings")]
    [SerializeField] private Vector2 attackBoxSize = new Vector2(1f, 0.5f);
    [SerializeField] private LayerMask hitLayer;

    [Header("References")]
    [SerializeField] private Transform attackPoint;

    // Internal State
    private bool isJumping = false;
    private bool canDoubleJump = false;
    private bool isAttacking = false;

    // Constants
    private static readonly int Transition = Animator.StringToHash("Transition");
    private static readonly int HitTrigger = Animator.StringToHash("Hit");
    private static readonly int DeathTrigger = Animator.StringToHash("Death");

    private void Awake()
    {
        // Cache Components
        rig2D = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            HandleJump();
        }

        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            HandleAttack();
        }
    }

    private void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        rig2D.linearVelocity = new Vector2(movement * speed, rig2D.linearVelocity.y);

        // Animation and Flip
        if (movement != 0)
        {
            if (!isJumping && !isAttacking)
            {
                SetAnimationState(1); // Running
            }

            transform.eulerAngles = (movement < 0) ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);
        }
        else if (!isJumping && !isAttacking)
        {
            SetAnimationState(0); // Idle
        }
    }

    private void HandleJump()
    {
        if (!isJumping)
        {
            PerformJump();
            isJumping = true;
            canDoubleJump = true;
        }
        else if (canDoubleJump)
        {
            PerformJump();
            canDoubleJump = false;
            SetAnimationState(3); // Double Jump Animation
        }
    }

    private void PerformJump()
    {
        rig2D.linearVelocity = new Vector2(rig2D.linearVelocity.x, 0); // Reset Y velocity
        rig2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        SetAnimationState(2); // Jump Animation
    }

    private void HandleAttack()
    {
        isAttacking = true;
        SetAnimationState(4); // Attack Animation

        Collider2D hit = Physics2D.OverlapBox(attackPoint.position, attackBoxSize, 0, hitLayer);
        if (hit != null)
        {
            var enemy = hit.GetComponent<Enemy>();
            enemy?.OnHit();

            var miscellaneous = hit.GetComponent<miscellaneousHit>();
            miscellaneous?.OnHit();
        }

        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.183f); // Attack animation duration
        isAttacking = false;

        if (isJumping)
        {
            SetAnimationState(3); // Return to Jump Animation
        }
    }

    private void SetAnimationState(int state)
    {
        animator.SetInteger(Transition, state);
    }

    public void OnHit()
    {
        animator.SetTrigger(HitTrigger);
        health--;
        GameController.instance.HandleHealth(health);

        if (health <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        animator.SetTrigger(DeathTrigger);
        speed = 0;
        Destroy(gameObject, 0.55f); // Wait for animation to finish
        GameController.instance.HandleLifes();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            OnHit();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Collectable"))
        {
            if (collision.CompareTag("Coin"))
            {
                GameController.instance.GetCoin();
            }
            else if (collision.CompareTag("Orb"))
            {
                GameController.instance.GetOrb();
            }

            collision.GetComponent<Animator>()?.SetTrigger("Pick");
            Destroy(collision.gameObject, 0.417f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackBoxSize);
    }
}
