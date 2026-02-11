using System;
using UnityEngine;

public class VectorEvent : MonoBehaviour
{
    public Action<Vector2> OnVectorEvent;
    
    public void InvokeOnVectorEvent(Vector2 vector) => OnVectorEvent?.Invoke(vector);
}
