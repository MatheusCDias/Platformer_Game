using UnityEngine;

public class miscellaneousHit : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private bool hitTwice;
    protected internal bool hitted;

    [SerializeField] private MiscellaneousType type;
    public float value;

    private void Awake()
    {
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

        if (!hitTwice)
        {
            hitted = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        switch (type)
        {
            case MiscellaneousType.Vase:
                HandleVase();
                break;

            default:
                Debug.Log("None");
                break;
        }
    }

    private void HandleVase()
    {
        GameObject coin = Instantiate(Resources.Load<GameObject>("Coin"), transform.position, Quaternion.identity);
        Rigidbody2D coinRb = coin.GetComponent<Rigidbody2D>();
        if (coinRb != null)
        {
            coinRb.AddForce(new Vector2(Random.Range(-1, 1), 5), ForceMode2D.Impulse);
        }
    }

    public enum MiscellaneousType
    {
        Vase,
        Lever
    }
}
