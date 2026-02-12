using System;
using System.Collections.Generic;
using UnityEngine;

public class FindAggro : EnemyMovementPattern
{
    [SerializeField] private EntityEvent onFindAggro;
    
    [HideInInspector] public bool HasAggro = false;
    [HideInInspector] public Vector2 AggroTarget = Vector2.zero;
    
    [Tooltip("The collider used as this entities vision")]
    [SerializeField] private Collider2D aggroCollider;
    [Tooltip("The layers of creatures that are hostile to this")]
    [SerializeField] private LayerMask hostileLayers;
    
    private Vector2 _defaultOffset;
    private Vector3 _defaultScale;
    private Vector2 _oppositeOffset;
    private Vector3 _oppositeScale;

    public override void Setup()
    {
        _defaultOffset = aggroCollider.offset;
        _oppositeOffset = new Vector2(-aggroCollider.offset.x, aggroCollider.offset.y);

    }

    public override void Execute(EntityMovement movement, LayerMask hostileLayers, GroundCheck groundCheck)
    {
        HandleAggro();
    }

    public void FlipVision(bool isStartDirection)
    {
        if (isStartDirection)
        {
            aggroCollider.offset = _defaultOffset;
        }
        else
        {
            aggroCollider.offset = _oppositeOffset;
        }
    }

    private void HandleAggro()
    {
        if (aggroCollider.IsTouchingLayers(hostileLayers))
        {
            List<Collider2D> contacts = new List<Collider2D>();
            aggroCollider.GetContacts(contacts);
            foreach (Collider2D contact in contacts)
            {
                if (((1 << contact.gameObject.layer) & hostileLayers) != 0)
                {
                    HasAggro = true;
                    AggroTarget = contact.transform.position;
                }
            }
            onFindAggro.InvokeOnVectorEvent(AggroTarget);
        }
    }
}
