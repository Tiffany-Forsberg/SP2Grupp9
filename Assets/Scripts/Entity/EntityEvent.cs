using System;
using UnityEngine;

public class EntityEvent : MonoBehaviour
{
    public Action<Vector2> OnVectorEvent;
    public Action OnEvent;
    
    public void InvokeOnVectorEvent(Vector2 vector) => OnVectorEvent?.Invoke(vector);
    public void InvokeOnEvent() => OnEvent?.Invoke();
}
