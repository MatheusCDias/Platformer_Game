using System.Drawing;
using UnityEngine;

public class Slime : Enemy
{
    public LayerMask groundLayer;
    public GameObject point;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Rig2D.linearVelocity = new Vector2(Speed, Rig2D.linearVelocityY);
        Collision();
    }

    void Collision()
    {
        Collider2D hit = Physics2D.OverlapCircle(point.transform.position, .05f, groundLayer);

        if (hit != null)
        {
            Debug.Log("Collide");
            this.Speed = -this.Speed;

            if (transform.eulerAngles.y == 0)
                transform.eulerAngles = new Vector3(0, 180, 0);
            else
                transform.eulerAngles = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.transform.position, .05f);
    }
}
