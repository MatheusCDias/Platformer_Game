using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rig2D;
    private BoxCollider2D bxCollider2D;
    private Animator anim;

    [SerializeField]
    private string enemyName;
    [SerializeField, Range(0, 5)]
    private int life;
    [SerializeField, Range(0, 5)]
    private float speed;
    [SerializeField, Range(0, 5)]
    private int damage;

    public Rigidbody2D Rig2D
    {
        get { return rig2D; }
        set { rig2D = value; }
    }

    public BoxCollider2D BxCollider2D
    {
        get { return bxCollider2D; }
        set { bxCollider2D = value; }
    }

    public Animator Anim
    {
        get { return anim; }
        set { anim = value; }
    }

    public string EnemyName
    {
        get { return enemyName; }
        set { enemyName = value; }
    }

    public int Life
    {
        get { return life; }
        set { life = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = Mathf.Max(value, 0); }    // Ensures that Speed ​​will never be negative
    }

    public int Damage
    {
        get { return damage; }
        set { damage = Mathf.Max(value, 0); }
    }


    private void Awake()
    {
        rig2D = GetComponent<Rigidbody2D>();
        bxCollider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnHit()
    {
        anim.SetTrigger("Hit");
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
        anim.SetTrigger("Death");
        Destroy(gameObject, 0.417f);
    }
}
