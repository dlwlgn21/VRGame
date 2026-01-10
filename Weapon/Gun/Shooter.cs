using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Shooter : MonoBehaviour
{
    public UnityEvent<Vector3> OnShootSuccessEvent;
    public UnityEvent OnShootFailedEvent;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] GameObject _hitEffectPrefab;
    [SerializeField] private Transform _shootPoint;

    [SerializeField] private float _shootDelayInSec = 0.1f;
    [SerializeField] private float _maxDist = 100f;

    private Magazine _magazine;

    private void Awake()
    {
        _magazine = GetComponent<Magazine>();
    }

    void Start()
    {
        Stop();
    }

    public void StartShootingProcess()
    {
        StopAllCoroutines();
        StartCoroutine(StartShootingProcess_Co());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator StartShootingProcess_Co()
    {
        WaitForSeconds waitTimeInSec = new(_shootDelayInSec);
        while (true)
        {
            if (_magazine.TryUseBullet())
            {
                Shoot();
            }
            else
            {
                OnShootFailedEvent?.Invoke();
            }
            yield return waitTimeInSec;
        }
    }

    private void Shoot()
    {
        if (Physics.Raycast(_shootPoint.position, _shootPoint.forward, out RaycastHit hitInfo, _maxDist, _layerMask))
        {
            Instantiate(_hitEffectPrefab, hitInfo.point, Quaternion.identity);
            var hittable = hitInfo.transform.GetComponent<Hittable>();
            if (hittable != null)
            {
                hittable.CallHitEvent();
            }
            OnShootSuccessEvent?.Invoke(hitInfo.point);
        }
        else
        {
            Vector3 maxDistPoint = _shootPoint.position + _shootPoint.forward * _maxDist;
            OnShootSuccessEvent?.Invoke(maxDistPoint);
        }
    }
}
