using UnityEngine;

public class strangeDoor : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private Animator anim;

    public OpenSystem openSystem;
    [SerializeField]
    private GameObject openSystemObject;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OpeningDoor();
    }

    void OpeningDoor()
    {
        switch (openSystem)
        {
            case OpenSystem.Lever:
                if (this.openSystemObject.GetComponent<miscellaneousHit>().hitted && Input.GetButtonDown("Fire1"))
                {
                    anim.SetTrigger("Open");
                    boxCollider2D.enabled = false;
                    this.openSystemObject.GetComponent<miscellaneousHit>().hitted = false;
                }
                break;
        }
        
    }

    public enum OpenSystem
    {
        Lever,
        Button,
    }
}
