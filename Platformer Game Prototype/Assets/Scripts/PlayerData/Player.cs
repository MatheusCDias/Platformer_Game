
﻿using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig2D;
    private SpriteRenderer sprite;
    private Animator animator;

    [SerializeField, Range(0,5)]
    private int life;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    private float b = 1;
    private float h = .57f;

    private bool isJumping;
    private bool doubleJump;
    private bool isAttacking;

    [SerializeField]
    private LayerMask hitLayer;

    public GameObject point;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        point = GameObject.Find("Player Point");
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
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (movement > 0)
        {
            if (!isJumping && !isAttacking)
                animator.SetInteger("Transition", 1);
            transform.eulerAngles = new Vector3(0, 0, 0);
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
            Collider2D hit = Physics2D.OverlapBox(point.transform.position, new Vector2(b, h), 0, hitLayer);

            animator.SetInteger("Transition", 4);

            if (hit != null)
            {
                if (hit.GetComponent<Enemy>() != null)
                    hit.GetComponent<Enemy>().OnHit();

                if (hit.GetComponent<miscellaneousHit>() != null)
                    hit.GetComponent<miscellaneousHit>().OnHit();
            }

            StartCoroutine("OnAttack");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(point.transform.position, new Vector3(b, h, 0));
    }

    IEnumerator OnAttack()
    {
        yield return new WaitForSeconds(0.183f);
        isAttacking = false;

        if (isJumping)
        {
            animator.SetInteger("Transition", 3);
        }
    }

    void OnHit()
    {
        animator.SetTrigger("Hit");
        life--;

        if (life <= 0)
        {
            life = 0;
            speed = 0;
            Death();
        }
    }

    void Death()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject, 0.55f);
        // Game Over
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OnHit();
        }
    }
}
