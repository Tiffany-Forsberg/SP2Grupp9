using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Tooltip("The size of the ground check box cast")]
    [SerializeField] private Vector2 groundCastSize;
    
    private Vector2 _groundDirection = Vector2.down;
    
    [SerializeField] private LayerMask groundLayer;

    void FixedUpdate()
    {
        // TODO: REPLACE WITH COYOTE TIMER LOGIC
        if (CheckGrounded())
        {
            Debug.Log("Ground Check: TRUE");
        }
        else
        {
            Debug.Log("Ground Check: FALSE");
        }
    }    
    public bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, groundCastSize, 0, _groundDirection, groundCastSize.y, groundLayer);
        return hit;
    }
    
    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, groundCastSize);
    }
}
