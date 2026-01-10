using System.Collections;
using UnityEngine;

public class LineFlasher : MonoBehaviour
{
    [SerializeField] private float _flashDuration = 0.05f;

    private LineRenderer _targetLineRenderer;

    private void Awake()
    {
        _targetLineRenderer = GetComponent<LineRenderer>();
    }

    public void StartFlash()
    {
        StopAllCoroutines();
        StartCoroutine(StartFlash_Co());
    }

    private IEnumerator StartFlash_Co()
    {
        _targetLineRenderer.enabled = true;

        yield return new WaitForSeconds(_flashDuration);

        _targetLineRenderer.enabled = false;
    }
}
