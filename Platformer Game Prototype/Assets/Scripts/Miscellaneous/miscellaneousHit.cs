using UnityEngine;

public class miscellaneousHit : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private bool hitTwice;

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
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
