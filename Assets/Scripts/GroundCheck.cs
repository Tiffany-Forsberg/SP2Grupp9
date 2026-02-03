using System;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Tooltip("The size of the ground check box cast")]
    [SerializeField] private Vector2 groundCastSize;
    
    private Vector2 _groundDirection = Vector2.down;
    
    [SerializeField] private LayerMask groundLayer;

    [SerializeField, Range(0f, 1f)] private float groundNormalTolerance;

    private bool _onGround => _groundObjects.Count > 0;

    private List<GameObject> _groundObjects;

    private void Awake()
    {
        _groundObjects = new List<GameObject>();
    }

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
        return _onGround;

        /*RaycastHit2D hit = Physics2D.BoxCast(transform.position, groundCastSize, 0, _groundDirection, groundCastSize.y, groundLayer);
        return hit;*/
    }
    
    // Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, groundCastSize);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(((1<<other.gameObject.layer) & groundLayer) != 0)
        {
            Vector2 averageGroundNormal = new Vector2();

            for (int i = 0; i < other.contactCount; i++)
            {
                averageGroundNormal += other.contacts[i].normal;
            }
        
            averageGroundNormal /= other.contactCount;
        
            averageGroundNormal.Normalize();

            if (averageGroundNormal.y >= groundNormalTolerance)
            {
                _groundObjects.Add(other.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (_groundObjects.Contains(other.gameObject))
        {
            _groundObjects.Remove(other.gameObject);
        }
    }
}
