using UnityEngine;
using static PlayerInput;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private ScriptableObject scriptableStats;

    private Rigidbody2D rig2D;
    private CapsuleCollider2D coll2D;

    private void Awake()
    {
        rig2D = GetComponent<Rigidbody2D>();
        coll2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Input
    protected internal FrameInput frameinput;

    void GatherInput()
    {
        
    }
    #endregion

    #region Horizontal Movement
    private void HandleDirection()
    {

    }
    #endregion
}
