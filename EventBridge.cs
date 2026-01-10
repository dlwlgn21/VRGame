using UnityEngine;
using UnityEngine.Events;

public class EventBridge : MonoBehaviour
{
    public UnityEvent OnCallEvent;

    public void Call()
    {
        OnCallEvent?.Invoke();
    }
}
