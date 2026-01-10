using UnityEngine;

public class ActivateOnLookat : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private Behaviour _target;

    [SerializeField] private float _thresholdAngleToActivate;
    [SerializeField] private float _thresholdDurationToActivate;

    private bool _bIsLooking = false;
    private float _showingTimeInSec;


    private void Awake()
    {
        _target.enabled = false;
    }

    private void Update()
    {
        Vector3 toTargetDir = _target.transform.position - _camera.transform.position;
        float angle = Vector3.Angle(_camera.transform.forward, toTargetDir);

        if (angle <= _thresholdAngleToActivate)
        {
            if (!_bIsLooking)
            {
                _bIsLooking = true;
                _showingTimeInSec = Time.time + _thresholdDurationToActivate;
            }
            else
            {
                if (!_target.enabled && Time.time > _showingTimeInSec)
                {
                    _target.enabled = true;
                }
            }
        }
        else
        {
            if (_bIsLooking)
            {
                _bIsLooking = false;
                _target.enabled = false;
            }
        }
    }
}
