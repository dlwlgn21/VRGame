using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TeleportActionHandler : MonoBehaviour
{
    public UnityEvent OnShowEvent;
    public UnityEvent OnHideEvent;

    [SerializeField] private InputActionReference _inputActionRef;
    private void OnEnable()
    {
        _inputActionRef.action.performed += OnPerformed;
        _inputActionRef.action.canceled += OnCancled;
    }

    private void OnDisable()
    {
        _inputActionRef.action.performed -= OnPerformed;
        _inputActionRef.action.canceled -= OnCancled;
    }

    public void OnPerformed(InputAction.CallbackContext obj)
    {
        StartCoroutine(WaitOneFrameAndCallCallback_Co(OnShowEvent));
    }
    public void OnCancled(InputAction.CallbackContext obj)
    {
        StartCoroutine(WaitOneFrameAndCallCallback_Co(OnHideEvent));
    }

    private IEnumerator WaitOneFrameAndCallCallback_Co(UnityEvent callback)
    {
        yield return null;
        callback?.Invoke();
    }
}
