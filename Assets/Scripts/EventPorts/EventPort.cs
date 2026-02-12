using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "EventPort", menuName = "EventPorts/EventPort")]
public class EventPort : ScriptableObject
{
    public UnityAction OnEventRaised;
    
    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}
