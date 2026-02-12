using UnityEngine;
using ImprovedTimers;
using UnityEngine.Events;

/// <summary>
/// Moves in a helicopter-esque pattern that stays a certain distance within and away from the player.
/// Enemies with this movement pattern should not be affected by gravity and will fly
/// </summary>
public class HelicopterMovementPattern : EnemyMovementPattern
{
    [SerializeField] private EntityEvent firingEvent;
    
    private GameObject _playerObject;
    // THIS IS A TEMP SOLUTION, PLEASE FIND A BETTER WAY AND REMOVE THIS ASAP
    [Tooltip("The name of the player object, to be removed in favor of a better way asap")]
    [SerializeField] private string playerName;

    [Tooltip("This is not a vector, but a range that this enemy prefers to stay within relative to the player, X = min distance and Y = max distance")]
    [SerializeField] private Vector2 preferredRangeToPlayer;

    [SerializeField] private float speed;

    [SerializeField] private float moveCycleCooldown;
    private CountdownTimer moveCycleTimer;

    public override void Setup()
    {
        if (!_playerObject)
        {
            _playerObject = GameObject.Find(playerName);
        }
        
        moveCycleTimer = new CountdownTimer(moveCycleCooldown);
        moveCycleTimer.Reset();
        moveCycleTimer.Start();
    }

    public override void Execute(EntityMovement movement, LayerMask hostileLayers, GroundCheck groundCheck)
    {
        if (!_playerObject) return;

        Vector2 positionDelta = _playerObject.transform.position - movement.transform.position;
        Vector2 deltaDirection = positionDelta.normalized; // Towards player
        
        if (positionDelta.magnitude < preferredRangeToPlayer.x)
        {
            movement.ApplyDirectVelocity(-deltaDirection * speed);
        }
        else if (positionDelta.magnitude > preferredRangeToPlayer.y)
        {
            movement.ApplyDirectVelocity(deltaDirection * speed);
        }
        else
        {
            if (moveCycleTimer.IsFinished)
            {
                Vector2 randomMovement = Random.insideUnitCircle * speed;
                movement.ApplyDirectVelocity(randomMovement);
                moveCycleTimer.Reset();
                moveCycleTimer.Start();
                
                firingEvent.OnVectorEvent?.Invoke(deltaDirection);
            }
        }
    }
}
