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
    
}