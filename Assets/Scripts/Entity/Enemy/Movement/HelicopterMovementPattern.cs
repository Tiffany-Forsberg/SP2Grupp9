using UnityEngine;

public class HelicopterMovementPattern : EnemyMovementPattern
{
    private GameObject _playerObject;
    // THIS IS A TEMP SOLUTION, PLEASE FIND A BETTER WAY AND REMOVE THIS ASAP
    [Tooltip("The name of the player object, to be removed in favor of a better way asap")]
    [SerializeField] private string playerName;

    [SerializeField] private float preferredRangeToPlayer;

    [SerializeField] private float speed;

    public override void Setup()
    {
        if (!_playerObject)
        {
            _playerObject = GameObject.Find(playerName);
        }
    }

    public override void Execute(EntityMovement movement, LayerMask hostileLayers, GroundCheck groundCheck)
    {
        if (!_playerObject) return;

        Vector2 positionDelta = _playerObject.transform.position - movement.transform.position;
        Vector2 deltaDirection = positionDelta.normalized; // Towards player
        
        if (positionDelta.magnitude < preferredRangeToPlayer)
        {
            movement.ApplyDirectVelocity(-deltaDirection * speed);
        }
    }
}
