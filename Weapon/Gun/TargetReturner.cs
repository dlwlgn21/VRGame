using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TargetReturner : MonoBehaviour
{
    public UnityEvent OnReturnCompletedEvent;
    [SerializeField] private Transform _target;
    [SerializeField] private float _returnDuration = 1f;

    [SerializeField] private AnimationCurve _animCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    public void StartReturnToTarget()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        StopAllCoroutines();
        StartCoroutine(StartReturnToTarget_Co());
    }


    private IEnumerator StartReturnToTarget_Co()
    {
        if (_target == null)
        {
            yield break;
        }

        float beginTime = Time.time;

        while (true)
        {
            float t = (Time.time - beginTime) / _returnDuration;

            if (t >= 1f)
            {
                break;
            }
            t = _animCurve.Evaluate(t);

            transform.position = Vector3.Lerp(transform.position, _target.position, t);
            yield return null;
        }
        transform.position = _target.position;

        OnReturnCompletedEvent?.Invoke();
    }
}
