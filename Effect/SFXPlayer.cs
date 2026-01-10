using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private float _minPitch = 0.8f;
    [SerializeField] private float _maxPitch = 1.5f;

    private AudioSource _targetAudio;


    private void Awake()
    {
        _targetAudio = GetComponent<AudioSource>();
    }

    public void PlaySFX()
    {
        Debug.Assert(_targetAudio != null);
        _targetAudio.pitch = Random.Range(_minPitch, _maxPitch);
        _targetAudio.Play();
    }

}
