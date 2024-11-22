using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rig2D;
    private BoxCollider2D bxCollider2D;

    [SerializeField]
    private string enemyName;
    [SerializeField]
    private int life;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int damage;

    public Rigidbody2D Rig2D
    {
        get { return rig2D; }
        set { rig2D = value; }
    }

    public string EnemyName
    {
        get { return enemyName; }
        set { enemyName = value; }
    }

    public int Life
    {
        get { return life; }
        set
        {
            life = value;
            if (life <= 0)
            {
                life = 0;
                // Enemy dies
            }
        }
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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
