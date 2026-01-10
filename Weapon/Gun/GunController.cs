using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
public class GunController : MonoBehaviour
{
    public UnityEvent OnGrabEvent;
    public UnityEvent OnReleaseEvent;

    public void OnGrab(SelectEnterEventArgs args)
    {
        IXRSelectInteractor interactor = args.interactorObject;
        if (interactor is XRDirectInteractor)
        {
            OnGrabEvent?.Invoke();
        }
    }
    public void OnRelease(SelectExitEventArgs args)
    {
        IXRSelectInteractor interactor = args.interactorObject;
        if (interactor is XRDirectInteractor)
        {
            OnReleaseEvent?.Invoke();
        }
    }
}
