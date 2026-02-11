using System;
using System.Collections.Generic;
using UnityEngine;

public class FindAggro : EnemyMovementPattern
{
    [HideInInspector] public bool HasAggro = false;
    [HideInInspector] public Vector2 AggroTarget = Vector2.zero;
    
    [Tooltip("The collider used as this entities vision")]
    [SerializeField] private Collider2D aggroCollider;
    [Tooltip("The layers of creatures that are hostile to this")]
    [SerializeField] private LayerMask hostileLayers;

    public override void Execute(EntityMovement movement, LayerMask hostileLayers, GroundCheck groundCheck)
    {
        HandleAggro();
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
        }
    }
}
