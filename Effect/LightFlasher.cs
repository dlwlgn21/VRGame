using System.Collections;
using UnityEngine;

public class LightFlasher : MonoBehaviour
{
    [SerializeField] private float _flashDuration = 0.05f;

    private Light _targetLight;

    private void Awake()
    {
        _targetLight = GetComponent<Light>();
    }

    public void StartFlash()
    {
        StopAllCoroutines();
        StartCoroutine(StartFlash_Co());
    }

    private IEnumerator StartFlash_Co()
    {
        _targetLight.enabled = true;

        yield return new WaitForSeconds(_flashDuration);

        _targetLight.enabled = false;
    }
}
