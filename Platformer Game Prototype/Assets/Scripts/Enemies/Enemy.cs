using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Components
    protected Rigidbody2D rig2D;
    protected BoxCollider2D bxCollider2D;
    protected Animator anim;

    // Enemy Parameters
    [Header("Enemy Stats")]
    [SerializeField] private string enemyName;
    [SerializeField, Range(0, 5)] private int life = 3;
    [SerializeField, Range(0, 5)] private float speed = 1f;
    [SerializeField, Range(0, 5)] private int damage = 1;

    [Header("Death")]
    [SerializeField] private AnimationClip deathClip;

    // State
    protected bool isAlive = true;

    protected virtual void Awake()
    {
        // Cache components
        rig2D = GetComponent<Rigidbody2D>();
        bxCollider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    public virtual void OnHit(int damageTaken = 1)
    {
        if (!isAlive) return;

        life -= damageTaken;
        anim.SetTrigger("Hit");

        if (life <= 0)
        {
            isAlive = false;
            life = 0;
            speed = 0;
            Death();
        }
    }

    protected virtual void Death()
    {
        anim.SetTrigger("Death");
        Destroy(gameObject, deathClip != null ? deathClip.length : .5f); // Use default length if no clip
    }

    // Public API for movement
    public float GetSpeed() => isAlive ? speed : 0;
}
