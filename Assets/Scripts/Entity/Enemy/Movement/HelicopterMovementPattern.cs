using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImprovedTimers;
using UnityEngine.Events;
using Random = UnityEngine.Random;

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

    private Coroutine _moveTowardsPoint;
    private bool movingRoutineRunning;

    private List<Vector2> _cyclePoints;

    [Tooltip("The amount that selected cycle points around the player will be rotated from the current position's equilibrium point")]
    [SerializeField, Range(0f, 1f)] private float movePointRotation;

    public override void Setup()
    {
        if (!_playerObject)
        {
            _playerObject = GameObject.Find(playerName);
        }
        
        moveCycleTimer = new CountdownTimer(moveCycleCooldown);
        moveCycleTimer.Reset();
        moveCycleTimer.Start();
        
        _cyclePoints = new List<Vector2>();
    }

    public override void Execute(EntityMovement movement, LayerMask hostileLayers, GroundCheck groundCheck)
    {
        if (!_playerObject) return;

        Vector2 positionDelta = _playerObject.transform.position - movement.transform.position;
        Vector2 deltaDirection = positionDelta.normalized; // Towards player
        
        if (positionDelta.magnitude < preferredRangeToPlayer.x)
        {
            if (_moveTowardsPoint is not null || movingRoutineRunning)
            {
                movement.StopCoroutine(_moveTowardsPoint);
                movingRoutineRunning = false;
            }
            movement.ApplyDirectVelocity(-deltaDirection * speed);
        }
        else if (positionDelta.magnitude > preferredRangeToPlayer.y)
        {
            if (_moveTowardsPoint is not null || movingRoutineRunning)
            {
                movement.StopCoroutine(_moveTowardsPoint);
                movingRoutineRunning = false;
            }
            movement.ApplyDirectVelocity(deltaDirection * speed);
        }
        else
        {
            if (_moveTowardsPoint is null || !movingRoutineRunning)
            {
                FindPoints(positionDelta);
                foreach (Vector2 point in _cyclePoints)
                {
                    Debug.Log(point);
                }
                _moveTowardsPoint = movement.StartCoroutine(CycleBetweenPoints(movement));
                movingRoutineRunning = true;
            }
            
            /*if (moveCycleTimer.IsFinished)
            {
                Vector2 randomMovement = Random.insideUnitCircle * speed;
                movement.ApplyDirectVelocity(randomMovement);
                moveCycleTimer.Reset();
                moveCycleTimer.Start();
                
                firingEvent.OnVectorEvent?.Invoke(deltaDirection);
            }*/
        }
    }

    private void FindPoints(Vector2 positionDelta)
    {
        _cyclePoints.Clear();
        Vector2 midPoint = (-positionDelta).normalized * ((preferredRangeToPlayer.x + preferredRangeToPlayer.y)*0.5f);

        Vector2 xAxis = midPoint.normalized;
        Vector2 yAxis = new Vector2(xAxis.y, -xAxis.x);
        
        for (int i = 0; i < 3; i++)
        {
            float randomRotation = Random.Range(-movePointRotation, movePointRotation);
            Vector2 randomArchPoint = xAxis * Mathf.Cos(randomRotation) + yAxis * Mathf.Sin(randomRotation);
            _cyclePoints.Add(randomArchPoint * Random.Range(preferredRangeToPlayer.x, preferredRangeToPlayer.y));
        }
    }

    private IEnumerator CycleBetweenPoints(EntityMovement movement)
    {
        if (_cyclePoints == null)
        {
            throw new Exception(
                "Helicopter enemy entered position cycling without having assigned points to cycle between, this should not be possible");
        }

        for (int i = 0; i < _cyclePoints.Count; i++)
        {
            Vector2 startPoint = movement.transform.position;

            Vector2 delta = _cyclePoints[i] - startPoint;
            
            Vector2 moveDirection = delta.normalized;
            
            while (Vector2.Dot(moveDirection, _cyclePoints[i] - startPoint) > 0)
            {
                Debug.Log(Vector2.Dot(moveDirection, _cyclePoints[i] - startPoint));
                movement.ApplyDirectVelocity(moveDirection * speed);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
