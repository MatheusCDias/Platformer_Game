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

    }

    void OpeningDoor()
    {
        anim.SetTrigger("Open");
        boxCollider2D.enabled = false;
    }

    public enum OpenSystem
    {
        Lever,
        Button,
    }
}
