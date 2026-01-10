using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class BombController : MonoBehaviour
{
    public enum EBombState
    {
        Idle,
        Drop,
        Count
    }
    public UnityEvent OnExpolosionEvent;
    public UnityEvent OnRecycleEvent;

    [SerializeField] private float _expolosionRadius;
    [SerializeField] private LayerMask _hittableLayerMask;
    [SerializeField] private float _recycleDelayTimeInSec = 1f;

    private EBombState _eCurrState = EBombState.Idle;

    public void Drop()
    {
        _eCurrState = EBombState.Drop;
    }
    public void Throw()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.interactionManager.CancelInteractableSelection((IXRSelectInteractable)interactable);

        var rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0f, 150f, 300f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_eCurrState == EBombState.Idle)
        {
            return;
        }

        Expolde();
    }


    private void Expolde()
    {
        Collider[] overlaps = Physics.OverlapSphere(transform.position, _expolosionRadius, _hittableLayerMask, QueryTriggerInteraction.Collide);

        foreach(Collider overlap in overlaps)
        {
            Hittable hittable = overlap.GetComponent<Hittable>();
            if (hittable != null)
            {
                hittable.CallHitEvent();
            }
        }
        OnExpolosionEvent?.Invoke();
        StartCoroutine(StartRecycle_Co());
    }

    private IEnumerator StartRecycle_Co()
    {
        _eCurrState = EBombState.Idle;
        yield return new WaitForSeconds(_recycleDelayTimeInSec);
        OnRecycleEvent?.Invoke();
    }
}
