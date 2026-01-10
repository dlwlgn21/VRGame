using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class Magazine : MonoBehaviour, IReloadable
{
    public UnityEvent OnReloadStartEvent;
    public UnityEvent OnReloadEndEvent;
    public UnityEvent<int> OnBulletChagnedEvent;
    public UnityEvent<float> OnChargeChagnedEvent;

    [SerializeField] private int _maxBullet = 20;
    [SerializeField] private float _chargingTimeInSec = 2f;

    private int _currBullet;
    public int CurrentBullet
    {
        get { return _currBullet; }

        set
        {
            _currBullet = Mathf.Clamp(value, 0, _maxBullet);
            OnBulletChagnedEvent?.Invoke(_currBullet);
            OnChargeChagnedEvent?.Invoke((float)_currBullet / _maxBullet);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentBullet = _maxBullet;
    }

    public bool TryUseBullet(int amount = 1)
    {
        if (CurrentBullet >= amount)
        {
            CurrentBullet -= amount;
            return true;
        }
        return false;
    }

    public void StartReload()
    {
        StopAllCoroutines();
        StartCoroutine(StartReload_Co());
    }

    public void StopReload()
    {
        StopAllCoroutines();
    }

    private IEnumerator StartReload_Co()
    {
        OnReloadStartEvent?.Invoke();
        float beginTime = Time.time;
        int beginBullet = CurrentBullet;
        float enoughPercentage = 1f - ((float)CurrentBullet / _maxBullet);
        float enoughChargingTime = _chargingTimeInSec * enoughPercentage;
        while (true)
        {
            float t = (Time.time - beginTime) / enoughChargingTime;
            if (t >= 1f)
            {
                break;
            }

            CurrentBullet = (int)Mathf.Lerp(beginBullet, _maxBullet, t);

            yield return null;
        }
        CurrentBullet = _maxBullet;
        OnReloadEndEvent?.Invoke();
    }
}
