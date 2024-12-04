using UnityEngine;

public class miscellaneousHit : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private bool hitTwice;
    protected internal bool hitted;

    [SerializeField] private MiscellaneousType type;

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
            case MiscellaneousType.LootBox:
                HandleLootBox();
                break;
        }
    }

    private void HandleVase()
    {
        CoinPhysics();
    }

    public void HandleLootBox()
    {
        for (int i = 0; i < 3;  i++)
        {
            CoinPhysics();
        }
    }

    private void CoinPhysics()
    {
        GameObject coin = Instantiate(Resources.Load<GameObject>("Coin"), transform.position, Quaternion.identity);
        Rigidbody2D coinRb = coin.GetComponent<Rigidbody2D>();
        if (coinRb != null)
        {
            coinRb.AddForce(new Vector2(Random.Range(-1f, 1f), 5), ForceMode2D.Impulse);
        }
    }

    public enum MiscellaneousType
    {
        Vase,
        Lever,
        LootBox
    }
}
