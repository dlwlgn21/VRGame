using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class WeaponStandController : MonoBehaviour
{
    public void OnSocketed(SelectEnterEventArgs args)
    {
        IReloadable reloadable = args.interactableObject.transform.GetComponent<IReloadable>();
        if (reloadable != null)
        {
            reloadable.StartReload();
        }
    }

    public void OnUnsocketed(SelectExitEventArgs args)
    {
        IReloadable reloadable = args.interactableObject.transform.GetComponent<IReloadable>();
        if (reloadable != null)
        {
            reloadable.StopReload();
        }
    }
}
