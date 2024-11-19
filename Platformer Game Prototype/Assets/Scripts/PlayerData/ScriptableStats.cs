using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableStats", menuName = "Scriptable Objects/ScriptableStats")]
public class ScriptableStats : ScriptableObject
{
    [Header("Player Scriptable Stats")]

    #region Layer 

    [Header("Layer"), Space]
    [Tooltip("Defines the layers of the respective GameObjects in the scene.")]
    public LayerMask playerLayer;
    public LayerMask groundLayer;

    #endregion

    #region Input

    [Header("Input"), Space]
    public bool snapInput = true;   // Makes all entries fit to an integer. Prevents gamepads from running slowly. The recommended value is true to ensure gamepad/keyboard parity.

    [Range(0.01f, 0.99f)]
    public float verticalDeadZoneThreshold = 0.3f;  // Minimum input required before climbing a ladder or climbing a ledge. Avoid unwanted climbs by using controllers.

    [Range(0.01f, 0.99f)]
    public float horizontalDeadZoneThreshold = 0.1f;    // Minimum input required before a left or right is recognized. Avoid drifting with sticky controls.

    #endregion

    [Header("Movement"), Space]
    public bool allowMove = true;
    public float speed; // The maximum speed of horizontal movement.
    public float acceleration;  // The Player's capacity to gain horizontal speed.
    public float groundDeceleration;    // The pace at which the player stops.
    public float airDeceleration;   // Deceleration in the air only after stopping the air entry.
    [Range(0f, -10f)]
    public float groundingForce;    // A constant downward force applied while grounded. Help on slopes.
    [Range(0f, 0.5f)]
    public float grounderDistance;  // The detection distance for grounding and roof detection.

}
