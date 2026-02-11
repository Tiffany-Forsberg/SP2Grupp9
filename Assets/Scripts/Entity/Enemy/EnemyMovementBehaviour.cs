using UnityEngine;

public class EnemyMovementBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask hostileLayers;
    [SerializeField] private EntityMovement movement;
    [SerializeField] private GroundCheck groundCheck;

    [SerializeReference]
    private EnemyMovementPattern pattern;

    private void Awake()
    {
        pattern.Setup();
    }
    
    private void FixedUpdate()
    {
        pattern.Execute(movement, hostileLayers, groundCheck);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        bool isSG = false;
        bool isDCOI = false;

        if (pattern.GetType() == typeof(SlashGruntMovementPattern))
        {
            isSG = true;
        }

        if (pattern.GetType() == typeof(DirectionChangeOnImpactPattern))
        {
            isDCOI = true;
        }

        if (isSG)
        {
            var p = (pattern as SlashGruntMovementPattern).directionChangeOnImpactPattern;
            Gizmos.DrawRay(p.rigidbody2D.position, p.Direction.normalized * p.raycastDistance);
        }

        if (isDCOI)
        {
            var p = pattern as DirectionChangeOnImpactPattern;
            Gizmos.DrawRay(p.rigidbody2D.position, p.Direction.normalized * p.raycastDistance);
        }        
    }
}