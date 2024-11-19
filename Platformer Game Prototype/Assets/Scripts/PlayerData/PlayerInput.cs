using UnityEngine;
using static UnityEditor.ShaderData;

public class PlayerInput : MonoBehaviour
{
    public FrameInput Gather()
    {
        return new FrameInput
        {
            Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")),
        };
    }

    public struct FrameInput
    {
        public Vector3 Move;
    }
}
