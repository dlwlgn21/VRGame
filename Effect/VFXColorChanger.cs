using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXColorChanger : MonoBehaviour
{
    [SerializeField] private float _arrangeRange = 0.5f;
    private ParticleSystem _targetParicle;

    private void Awake()
    {
        _targetParicle = GetComponent<ParticleSystem>();
    }
    public void SetParticleColor(Color color)
    {
        Debug.Assert(_targetParicle != null);
        var paricleMain = _targetParicle.main;
        paricleMain.startColor = new ParticleSystem.MinMaxGradient(
            color,
            color * Random.Range(1f - _arrangeRange, 1f + _arrangeRange)
        );
    }
}
