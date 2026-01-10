using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayVisualizer : MonoBehaviour
{
    [Header("Ray")]
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private LayerMask _hitLayer;
    [SerializeField] private float _rayDist = 100f;

    [Header("Reticle Point")]
    [SerializeField] private GameObject _reticlePoint;
    [SerializeField] private bool _isShowReticle = true;

    private readonly RaycastHit[] _raycastHits = new RaycastHit[4];
    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        StopAllCoroutines();
        StartCoroutine(StartRayCast_Co());
    }

    public void Hide()
    {
        StopAllCoroutines();
        SetNoVisible();
    }

    private IEnumerator StartRayCast_Co()
    {
        while (true)
        {
            int hitCount = Physics.RaycastNonAlloc(
                transform.position,
                transform.forward,
                _raycastHits,
                _rayDist,
                _hitLayer
            );
            if (hitCount > 0)
            {
                // 가장 가까운 히트 선택
                int closestIndex = 0;
                float closestDist = _raycastHits[0].distance;

                for (int i = 1; i < hitCount; i++)
                {
                    if (_raycastHits[i].distance < closestDist)
                    {
                        closestDist = _raycastHits[i].distance;
                        closestIndex = i;
                    }
                }

                Vector3 hitPoint = _raycastHits[closestIndex].point;
                _lineRenderer.SetPosition(1, transform.InverseTransformPoint(hitPoint));
                _lineRenderer.enabled = true;
                _reticlePoint.transform.position = hitPoint;
                _reticlePoint.SetActive(_isShowReticle);
            }
            else
            {
                _lineRenderer.enabled = false;
                _reticlePoint.SetActive(false);
            }

            yield return null;
        }
    }

    private void SetNoVisible()
    {
        _lineRenderer.enabled = false;
        _reticlePoint.SetActive(false);
    }
}
