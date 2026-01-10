using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeFlasher : MonoBehaviour
{
    [SerializeField] private float _flashDuration = 0.05f;

    private Volume _targetVolume;

    private void Awake()
    {
        _targetVolume = GetComponent<Volume>();
    }

    public void StartFlash()
    {
        StopAllCoroutines();
        StartCoroutine(StartFlash_Co());
    }

    private IEnumerator StartFlash_Co()
    {
        _targetVolume.enabled = true;

        yield return new WaitForSeconds(_flashDuration);

        _targetVolume.enabled = false;
    }
}
