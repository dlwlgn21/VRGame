using UnityEngine;
using UnityEngine.Events;

public class Hittable : MonoBehaviour
{
    public UnityEvent OnHitEvent;

    public void CallHitEvent()
    {
        OnHitEvent?.Invoke();
    }
}
