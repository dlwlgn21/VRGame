using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class HapticOnInteracablePlayer : MonoBehaviour
{
    [SerializeField] private float _amplitude = 0.5f;
    [SerializeField] private float _duration = 0.05f;

    private XRBaseInteractable _targetInteractable;

    private void Awake()
    {
        _targetInteractable = GetComponent<XRBaseInteractable>();
    }

    public void StartHaptic()
    {
        if (_targetInteractable == null ||
            _targetInteractable.firstInteractorSelecting == null ||
            _targetInteractable.firstInteractorSelecting is not XRBaseControllerInteractor)
        {
            return;
        }

        var interactor = _targetInteractable.firstInteractorSelecting as XRBaseControllerInteractor;
        if (interactor.xrController == null)
        {
            return;
        }

        interactor.xrController.SendHapticImpulse(_amplitude, _duration);
    }
}
