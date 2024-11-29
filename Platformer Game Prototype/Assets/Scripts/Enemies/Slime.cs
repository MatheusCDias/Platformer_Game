using UnityEngine;

public class Slime : Enemy
{
    [Header("Slime Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform collisionPoint;
    [SerializeField] private float collisionRadius = 0.1f;

    private float currentSpeed;

    protected override void Awake()
    {
        base.Awake();
        currentSpeed = GetSpeed();

        // Invert direction if facing left
        if (transform.eulerAngles.y == 0)
        {
            currentSpeed = -currentSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (!isAlive) return;

        // Movement logic
        rig2D.linearVelocity = new Vector2(currentSpeed, rig2D.linearVelocityY);

        // Handle collisions
        CheckCollision();
    }

    private void CheckCollision()
    {
        if (collisionPoint == null)
        {
            Debug.LogWarning($"{name} is missing a collision point!");
            return;
        }

        Collider2D hit = Physics2D.OverlapCircle(collisionPoint.position, collisionRadius, groundLayer);

        if (hit != null)
        {
            FlipDirection();
        }
    }

    private void FlipDirection()
    {
        currentSpeed = -currentSpeed;

        // Rotate sprite based on direction
        transform.eulerAngles = (transform.eulerAngles.y == 0) ? new Vector3(0, 180, 0) : Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        if (collisionPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(collisionPoint.position, collisionRadius);
        }
    }

    protected override void Death()
    {
        base.Death();
    }
}
